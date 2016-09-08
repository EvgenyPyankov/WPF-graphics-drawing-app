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
        private byte red = 34;
        private byte green = 200;
        private byte blue = 234;
        private Color color;

        public ColorWindow()
        {
            InitializeComponent();
        }

        public Color getColor()
        {
            return color;
        }

        private void Scroll(object sender, RoutedEventArgs e)
        {
            //rScrollLabel.Content = "hello";
            FrameworkElement element = e.Source as FrameworkElement;
            switch (element.Name)
            {
                case "rScroll":
                    rScrollLabel.Content = "255";
                    break;
                case "gScroll":
                    gScrollLabel.Content = "255";
                    break;
                case "bScroll":
                    bScrollLabel.Content = "255";
                    break;
            }
           // rScrollLabel.Content = e.ToString();
        }
    }
}
