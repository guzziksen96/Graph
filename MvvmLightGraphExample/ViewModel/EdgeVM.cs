using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightGraphExample.ViewModel
{
    public class EdgeVM : ViewModelBase
    {
        private NodeVM _startNode;

        public NodeVM StartNode
        {
            get { return _startNode; }
            set { _startNode = value;
                  RaisePropertyChanged();
            }
        }

        private NodeVM _endNode;

        public NodeVM EndNode
        {
            get { return _endNode; }
            set { _endNode = value;
                RaisePropertyChanged();
            }
        }

        public EdgeVM(NodeVM startNode, NodeVM endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        public EdgeVM()
        {

        }
    }
}
