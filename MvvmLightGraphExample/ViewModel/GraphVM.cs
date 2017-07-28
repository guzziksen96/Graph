using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace MvvmLightGraphExample.ViewModel
{
    public class GraphVM : ViewModelBase
    {
        private NodeVM startNode = null;
        private NodeVM endNode = null;
        private Point? previousPoint = null;
        private bool isDragginNode = false;

        List<NodeVM> listOfChosenNodes = new List<NodeVM>();

        private bool _isAddEdge;
        public bool IsAddEdge
        {
            get { return _isAddEdge; }
            set
            {
                _isAddEdge = value;
                RaisePropertyChanged();
            }
        }

        private bool _isAddNode;

        public bool IsAddNode
        {
            get { return _isAddNode; }
            set
            {
                _isAddNode = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<NodeVM> Nodes { get; private set; }
        public ObservableCollection<EdgeVM> Edges { get; private set; }

        private int nextIndex;
        private EdgeVM edge = null;

        public GraphVM()
        {
            this.Nodes = new ObservableCollection<NodeVM>();
            this.Edges = new ObservableCollection<EdgeVM>();
            nextIndex = 1;
        }
        public ICommand EdgeAddCommand => new RelayCommand<MouseButtonEventArgs>(AddEdge);
        private void AddEdge(MouseButtonEventArgs e)
        {
            if (IsAddEdge == true)
            {
                if (edge.StartNode != null && edge.EndNode != null && edge.StartNode != edge.EndNode)
                {
                    Edges.Add(edge);
                    edge = null;
                }
            }
        }

        public void AddEdge(NodeVM startNode, NodeVM endNode)
        {
            this.Edges.Add(new EdgeVM(startNode, endNode));
        }
        public ICommand AddNodeCommand => new RelayCommand<MouseButtonEventArgs>(AddNode);

        private void AddNode(MouseButtonEventArgs e)
        {
            if (IsAddNode == true)
            {
                if (e.OriginalSource is Canvas)
                {
                    var position = e.GetPosition(e.OriginalSource as Canvas);
                    Nodes.Add(new NodeVM(nextIndex, position.X, position.Y) { Size = 50 });
                    nextIndex++;
                }
            }
        }
        public ICommand MouseLeftButtonDownNodeToAddEdgeCommand => new RelayCommand<NodeVM>(MouseLeftButtonDownNodeToAddEdge);
        private void MouseLeftButtonDownNodeToAddEdge(NodeVM node)
        {
            if (IsAddEdge == true)
            {
                if (startNode != null)
                {
                    endNode = node;
                    AddEdge(startNode, endNode);
                    startNode = null;
                    endNode = null;
                }
                else
                {
                    startNode = node;
                }
            }
        }
        public ICommand NodeMouseMovedCommand => new RelayCommand<MouseEventArgs>(NodeMouseMoved);
        private void NodeMouseMoved(MouseEventArgs e)
        {
            Point point = e.GetPosition(e.OriginalSource as FrameworkElement);
            if (isDragginNode)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    NodeVM node = (e.OriginalSource as FrameworkElement).DataContext as NodeVM;
                    if (previousPoint.HasValue)
                    {
                        double xDiff = point.X - previousPoint.Value.X;
                        double yDiff = point.Y - previousPoint.Value.Y;

                        node.X += xDiff;
                        node.Y += yDiff;

                        node.XCenter += xDiff;
                        node.YCenter += yDiff;

                        if (listOfChosenNodes.Count > 1)
                        {
                            foreach (var n in listOfChosenNodes)
                            {
                                n.X += xDiff;
                                n.Y += yDiff;

                                n.XCenter += xDiff;
                                n.YCenter += yDiff;

                            }
                        }
                    }
                }
            }
        }

        public ICommand NodeMouseLeftButtonDownCommand => new RelayCommand<MouseButtonEventArgs>(NodeMouseLeftButtonDown);
        private void NodeMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //dodaje do listy node gdy shift jest zaznaczony

            if (IsAddNode == true)
            {
                var element = e.OriginalSource as FrameworkElement;
                var node = element.DataContext as NodeVM;
                var position = e.GetPosition(e.OriginalSource as FrameworkElement);
                previousPoint = position;
                isDragginNode = true;

                element.CaptureMouse();

                node.IsSelected = !node.IsSelected;
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {

                }
                //listOfChosenNodes.Add(e.OriginalSource as NodeVM);
                var selected = Nodes.Where(f => f.IsSelected).ToList();
                listOfChosenNodes = selected;
                startNode = null;
                endNode = null;
            }
        }

        public ICommand NodeMouseLeftButtonUpCommand => new RelayCommand<MouseButtonEventArgs>(NodeMouseLeftButtonUp);
        private void NodeMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            (e.OriginalSource as FrameworkElement).ReleaseMouseCapture();
            if (IsAddNode == true)
            {
                isDragginNode = false;
                previousPoint = null;
            }
        }

        public ICommand UnSelectAllNodesMouseDownCommand => new RelayCommand<MouseButtonEventArgs>(UnSelectAllNodes);

        private void UnSelectAllNodes(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                foreach (var node in listOfChosenNodes)
                {
                    node.IsSelected = false;
                }
            }
        }

        public ICommand ZoomCommand => new RelayCommand<MouseWheelEventArgs>(Zoom);

        private void Zoom(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                foreach (var node in listOfChosenNodes)
                {
                    node.Size += 1;
                }
            }
            else if (e.Delta < 0)
            {
                foreach (var node in listOfChosenNodes)
                {
                    node.Size -= 1;
                }
            }
        }
        public ICommand ZoomCanvasCommand => new RelayCommand<MouseWheelEventArgs>(ZoomCanvas);

        //TODO Zooming
        private void ZoomCanvas(MouseWheelEventArgs obj)
        {
            if (listOfChosenNodes.Count == 0)
            {
                var canvas = obj.OriginalSource as Canvas;
                //double height = canvas.ActualHeight;
                //double width = canvas.ActualWidth;
                double zoom = obj.Delta;
                //height += 2;
                //width += 2;
                //ScaleTransform sc = new ScaleTransform(width, height);
                //canvas.LayoutTransform = sc;
                //canvas.UpdateLayout();

                if (obj.Delta > 0)
                {
                    zoom += 1;
                }
                else if (obj.Delta < 0)
                {
                    zoom -= 1;
                }
            }
        }

        public ICommand DeleteNodeMouseRightButtonDownCommand => new RelayCommand<MouseButtonEventArgs>(DelateNode);

        private void DelateNode(MouseButtonEventArgs obj)
        {
            var element = obj.OriginalSource as FrameworkElement;
            var node = element.DataContext as NodeVM;

            List<EdgeVM> listOfEdgesToDelateNode = Edges.Where(e => e.EndNode == node || e.StartNode == node).ToList();

            Nodes.Remove(node);

            foreach (var edge in listOfEdgesToDelateNode)
            {
                Edges.Remove(edge);
            }
        }
    }
}