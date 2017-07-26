using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLightGraphExample
{
    //Attached behaviors are defined as a static class with one or more Attached Properties defined within it. 
    //An Attached Property can define a change callback handler for when the property gets set on some target element, 
    //and the callback handler gets passed a reference to the element on which it is being attached (much like the “this” 
    //parameter reference in an extension method) and an argument that tells what the old and new values for the property are. 

    internal static class Behaviours
    {
        public static readonly DependencyProperty SaveCanvasProperty =
            DependencyProperty.RegisterAttached("SaveCanvas", typeof(bool), typeof(Behaviours),
                new UIPropertyMetadata(false, OnSaveCanvas));

        public static void SetSaveCanvas(DependencyObject obj, bool value)
        {
            obj.SetValue(SaveCanvasProperty, value);
        }

        public static bool GetSaveCanvas(DependencyObject obj)
        {
            return (bool)obj.GetValue(SaveCanvasProperty);
        }

        private static void OnSaveCanvas(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                // Save code.....
            }
        }
    }
}
