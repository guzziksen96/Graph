using System.Windows;
using MvvmLightGraphExample.ViewModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace MvvmLightGraphExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        public void ConvertToBitmapSource(UIElement element, string fileName)
        {
            var target = new RenderTargetBitmap(
            (int)element.RenderSize.Width, (int)element.RenderSize.Height + 100,
            96, 96, PixelFormats.Pbgra32);
            target.Render(element);

            var encoder = new PngBitmapEncoder();
            var outputFrame = BitmapFrame.Create(target);
            encoder.Frames.Add(outputFrame);


            using (var file = File.OpenWrite("C:\\Users\\PLKLGUZ\\Downloads\\"+fileName+".png"))
            {
                encoder.Save(file);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertToBitmapSource(listBoxNodes as UIElement, textBox.Text);
        }
    }


}