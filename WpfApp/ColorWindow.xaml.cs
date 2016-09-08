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
        public ColorWindow()
        {
            InitializeComponent();
        }

        private void Scroll(object sender, RoutedEventArgs e)
        {
            //RScrollLabel.Content = "hello";
            FrameworkElement element = e.Source as FrameworkElement;
            switch (element.Name)
            {
                case "RScroll":
                    RScrollLabel.Content = "255";
                    break;
                case "GScroll":
                    GScrollLabel.Content = "255";
                    break;
                case "BScroll":
                    BScrollLabel.Content = "255";
                    break;
            }
           // RScrollLabel.Content = e.ToString();
        }
    }
}
