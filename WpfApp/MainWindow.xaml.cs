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
            //TODO min windows size
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
            DrawGraphic();   
        }

        private void DrawGraphic()
        {
            canvas.Children.Clear();
            //double x = this.Width;
            //double y = this.Height;
            //Line myLine = new Line();
            SolidColorBrush redBrush = new SolidColorBrush();
            Color color = Color.FromRgb(100, 200, 34);
            redBrush.Color = color;
            //myLine.Stroke = redBrush;
            //myLine.X1 = 1;
            //myLine.X2 = x / 2;
            //myLine.Y1 = 1;
            //myLine.Y2 = y / 2;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //myLine.StrokeThickness = 2;
            //canvas.Children.Add(myLine);

            Polyline polyline = new Polyline();
            polyline.Stroke = redBrush;
            System.Windows.Point Point1 = new System.Windows.Point(10, 100);
            System.Windows.Point Point2 = new System.Windows.Point(100, 200);
            System.Windows.Point Point3 = new System.Windows.Point(200, 30);
            System.Windows.Point Point4 = new System.Windows.Point(250, 200);
            System.Windows.Point Point5 = new System.Windows.Point(200, 150);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            polygonPoints.Add(Point4);
            polygonPoints.Add(Point5);
            polyline.Points = polygonPoints;
            polyline.StrokeThickness = 3;
            canvas.Children.Add(polyline);
        }

        private void Repaint(object sender, RoutedEventArgs e)
        {
            DrawGraphic();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            if (aboutWindow.ShowDialog() == true)
            {
                MessageBox.Show("yes ");
            }
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
