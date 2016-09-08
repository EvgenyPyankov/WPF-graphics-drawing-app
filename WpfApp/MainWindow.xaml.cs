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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
           

        }

        private void InputClick(object sender, RoutedEventArgs e)
        {
           
        }

        private void ColorClick(object sender, RoutedEventArgs e)
        {
            ColorWindow colorWindow = new ColorWindow();
            colorWindow.Show();
        }

        private void DrawClick(object sender, RoutedEventArgs e)
        {

        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
