using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            MouseWheel += MainWindow_MouseWheel;
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

        void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int delta;
            if (e.Delta > 0)
                delta = 1;
            else
                delta = -1;

            FrameworkElement element = FocusManager.GetFocusedElement(this) as FrameworkElement;
            int buf = 0;
            switch (element.Name)
            {
                case "rScroll":
                    if ((red > 0 && delta < 0) || (red < 255 && delta > 0))
                    {
                        buf = red;
                        buf += delta;
                        red = (byte)buf;
                        rScroll.Value = red;
                    }
                    break;
                case "gScroll":
                    if ((green > 0 && delta < 0) || (green < 255 && delta > 0))
                    {
                        buf = green;
                        buf += delta;
                        green = (byte)buf;
                        gScroll.Value = green;
                    }
                    break;
                case "bScroll":
                    if ((blue > 0 && delta < 0) || (blue < 255 && delta > 0))
                    {
                        buf = blue;
                        buf += delta;
                        blue = (byte)buf;
                        bScroll.Value = blue;
                    }
                    break;
            }
            color.R = red;
            color.G = green;
            color.B = blue;
            drawColorWindow();
        }
    }
}
