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
        private double[] x;
        private double[] y;

        private double width;
        private double height;
        private double marginX;
        private double marginY;
        private double stepX;
        private double stepY;
        private double minX;
        private double minY;
        private double maxX;
        private double maxY;
        private double x0;
        private double y0;

        private SolidColorBrush axisBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));

        public MainWindow()
        {
            //TODO min windows size
            //TODO refactor properties instead of methods
            //TODO delete unused imports
            InitializeComponent();
            Init();
        }

        private void Init()
        {
        }

        private void DrawGraphic(PointCollection polylinePoints)
        {
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
            //System.Windows.Point Point1 = new System.Windows.Point(10, 100);
            //System.Windows.Point Point2 = new System.Windows.Point(100, 200);
            //System.Windows.Point Point3 = new System.Windows.Point(200, 30);
            //System.Windows.Point Point4 = new System.Windows.Point(250, 200);
            //System.Windows.Point Point5 = new System.Windows.Point(200, 150);
            //PointCollection polygonPoints = new PointCollection();
            //polygonPoints.Add(Point1);
            //polygonPoints.Add(Point2);
            //polygonPoints.Add(Point3);
            //polygonPoints.Add(Point4);
            //polygonPoints.Add(Point5);
            polyline.Points = polylinePoints;
            polyline.StrokeThickness = 3;
            canvas.Children.Add(polyline);
        }

        private void DrawLines(List<double[]> lines, Brush brush)
        {
            foreach (double[] points in lines)
            {
                Line line = new Line();
                line.Stroke = brush;
                line.StrokeThickness = 0.5;
                line.X1 = points[0];
                line.Y1 = points[1];
                line.X2 = points[2];
                line.Y2 = points[3];
                canvas.Children.Add(line);

            }
        }

        private void DrawAxis()
        {
            List<double[]> lines = new List<double[]>();
            lines.Add(new double[] { marginX, y0, width - marginX, y0 });
            lines.Add(new double[] { x0, marginY, x0, height - marginY });
            DrawLines(lines, axisBrush);
        }

        private void CalculateValues()
        {
            width = canvas.ActualWidth;
            height = canvas.ActualHeight;
            marginX = width / 10;
            marginY = height / 10;
            stepX = (width - marginX * 4) / Math.Abs(maxX - minX);
            stepY = (height - marginY * 4) / Math.Abs(minY - maxY);
            x0 = marginX * 2;
            if (x[0] < 0)
                x0 += Math.Abs(x[0]) * stepX;
            y0 = marginY * 2;
            if (maxY > 0)
                y0 += maxY * stepY;
            //MessageBox.Show(minX.ToString()+" "+minY.ToString()+" "+maxX.ToString() + " " + maxY.ToString());
        }

        private void Paint()
        {
            canvas.Children.Clear();
            CalculateValues();
            DrawAxis();
            DrawGraphic(calculatePoints());
        }

        private PointCollection calculatePoints()
        {
            PointCollection polylinePoints = new PointCollection();
            for (int i = 0; i < x.Length; i++)
            {
                polylinePoints.Add(calculatePoint(x[i], y[i]));
            }
            return polylinePoints;
        }

        private Point calculatePoint(double x, double y)
        {
            return new Point(x0 + x * stepX, y0 - y * stepY);
        }

        private void Repaint(object sender, RoutedEventArgs e)
        {
            if (x != null && y != null)
                Paint();
        }

        private void InputClick(object sender, RoutedEventArgs e)
        {
            Points points = new Points();
            x = points.getX();
            y = points.getY();
            minY = points.getMinY();
            maxY = points.getMaxY();
            minX = x[0];
            maxX = x[x.Length-1];
        }

        private void ColorClick(object sender, RoutedEventArgs e)
        {
            ColorWindow colorWindow = new ColorWindow();
            colorWindow.Show();
        }

        private void DrawClick(object sender, RoutedEventArgs e)
        {
            Paint();  
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
