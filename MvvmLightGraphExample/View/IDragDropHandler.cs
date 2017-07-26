using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightGraphExample.View
{
    public interface IDragDropHandler
    {
        void Dropped();
        void Moved(double x, double y);
    }
}
