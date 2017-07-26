using GalaSoft.MvvmLight;

namespace MvvmLightGraphExample.ViewModel
{
    public class NodeVM : ViewModelBase
    {
        private double _x;

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }

        private double _y;

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }

        private double _xCenter;

        public double XCenter
        {
            get { return _xCenter; }
            set { _xCenter = value;
                RaisePropertyChanged();

            }
        }

        private double _yCenter;

        public double YCenter
        {
            get { return _yCenter; }
            set { _yCenter = value;
                RaisePropertyChanged();
            }
        }
        private double _size;

        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        private double _index;

        public double Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged();
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }


        public NodeVM()
        {
        }

        public NodeVM(int index, double x, double y)
        {
            this.Index = index;
            this.Size = 50;
            this.X = x;
            this.Y = y;
            this.XCenter = x + Size / 2;
            this.YCenter = y + Size / 2;
        }
    }
}