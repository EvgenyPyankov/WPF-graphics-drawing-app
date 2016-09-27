using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private byte red;
        private byte green;
        private byte blue;


        private Color color;

        public ColorWindow()
        {
            InitializeComponent();
            Init();
        }

        public Color coulor
        {
            get
            {
                return color;
            }
        }

       
     



        private void Init()
        {
            red = (byte)Application.Current.Resources["R"];
            green = (byte)Application.Current.Resources["G"];
            blue = (byte)Application.Current.Resources["B"];
            rScroll.Value = red;
            gScroll.Value = green;
            bScroll.Value = blue;
            color = Color.FromRgb(red, green, blue);
 
                 
        }

        private void drawColorWindow()
        {
            canvas.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));
        }

        private void Scroll(object sender, RoutedEventArgs e)
        {
            byte value;
            FrameworkElement element = e.Source as FrameworkElement;
            switch (element.Name)
            {
                case "rScroll":
                    value = (byte)rScroll.Value;
                    red = value;
                    break;
                case "gScroll":
                    value = (byte)gScroll.Value;
                    green = value;
                    break;
                case "bScroll":
                    value = (byte)bScroll.Value;
                    blue = value;
                    break;
            }
            color.R = red;
            color.G = green;
            color.B = blue;
            drawColorWindow();
        }

        private void AcceptСlick(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["R"] = red;
            Application.Current.Resources["G"] = green;
            Application.Current.Resources["B"] = blue;
            this.DialogResult = true;
        }
    }
}
