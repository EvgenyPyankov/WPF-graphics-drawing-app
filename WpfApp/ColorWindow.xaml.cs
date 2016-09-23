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
        private byte red = 100;
        private byte green = 200;
        private byte blue = 34;


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
            rScroll.Value = red;
            gScroll.Value = green;
            bScroll.Value = blue;
            color = Color.FromRgb(red, green, blue);
            //MessageBox.Show(rScroll.Template.Resources["BorderBrush"].ToString());
            
            //Track track = (Track)rScroll.Template.FindName("track", rScroll);
            //track.Thumb.Background =  new SolidColorBrush(Color.FromRgb(red, green, blue));
          
            
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
                    value = (byte)rScroll.Value;
                   // rScrollLabel.Content = value;
                    red = value;
                    break;
                case "gScroll":
                    value = (byte)gScroll.Value;
                   // gScrollLabel.Content = value;
                    green = value;
                    break;
                case "bScroll":
                    value = (byte)bScroll.Value;
                    //bScrollLabel.Content = value;
                    blue = value;
                    break;
            }
            color.R = red;
            color.G = green;
            color.B = blue;
            drawColorWindow();
           // rScrollLabel.Content = e.ToString();
        }

        private void AcceptСlick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
