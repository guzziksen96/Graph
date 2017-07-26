using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MvvmLightGraphExample
{
    public class RandomBrushes
    {
        private static Random _rand = new Random();

        public static Brush GetRandomBrush()
        {
            List<Brush> _brushes = new List<Brush>();
            var props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propInfo in props)
            {
                _brushes.Add((Brush)propInfo.GetValue(null, null));
            }
            return _brushes[_rand.Next(_brushes.Count)];
        }
    }
}
