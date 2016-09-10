using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    public partial class ColorWindow : Window
    {
        private byte red = 100;
        private byte green = 200;
        private byte blue = 34;
        private Color color;

        public ColorWindow()
        {
            InitializeComponent();
            Init();
        }

        public Color getColor()
        {
            return color;
        }

        private void Init()
        {
            rScrollLabel.Content = red;
            gScrollLabel.Content = green;
            bScrollLabel.Content = blue;
            rScroll.Value = 1d / red;
            gScroll.Value = 1d / green;
            bScroll.Value = 1d / blue;
            drawColorWindow();
            
        }

        private void drawColorWindow()
        {
            canvas.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));
        }

        private void Scroll(object sender, RoutedEventArgs e)
        {
            //rScrollLabel.Content = "hello";
            byte value;
            FrameworkElement element = e.Source as FrameworkElement;
            switch (element.Name)
            {
                case "rScroll":
                    value = (byte)(255 * rScroll.Value);
                    rScrollLabel.Content = value;
                    red = value;
                    break;
                case "gScroll":
                    value = (byte)(255*gScroll.Value);
                    gScrollLabel.Content = value;
                    green = value;
                    break;
                case "bScroll":
                    value = (byte)(255 * bScroll.Value);
                    bScrollLabel.Content = value;
                    blue = value;
                    break;
            }
            drawColorWindow();
           // rScrollLabel.Content = e.ToString();
        }

        private void AcceptСlick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
