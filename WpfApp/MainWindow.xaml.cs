using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;

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
        private double arrayLength;
        private double axisStepX;
        private double axisStepY;
        private bool toDrawNet = false;

        private SolidColorBrush axisBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        private SolidColorBrush graphicBrush = new SolidColorBrush(Color.FromRgb(200, 100, 100));

        public MainWindow()
        {
            //TODO refactor properties instead of methods
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            drawMenuButton.IsEnabled = false;
            marginX = (double)this.Resources["MARGIN_X"];
            marginY = (double)this.Resources["MARGIN_Y"];
            arrayLength = (double)this.Resources["ARROW_LENGTH"];
        }

        private void DrawGraphic(PointCollection polylinePoints)
        {
 
            Polyline polyline = new Polyline();
            polyline.Stroke = graphicBrush;
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

        private void DrawNet()
        {
            List<double[]> lines = new List<double[]>();
            findAxisSteps();
            double cur;
            int n = (int)(x0-marginX*2 / axisStepX);
            cur = x0 - axisStepX * n;
            while (cur < width - marginX)
            {
                lines.Add(new double[] { cur, marginY, cur, height - marginY });
                cur += axisStepX;
            }


            n =(int)(y0-marginY*2 / axisStepY);
            cur = y0 - axisStepY * n;
            while (cur < height - marginY)
            {
                lines.Add(new double[] { marginX, cur, width - marginX, cur });
                cur += axisStepY;
            }

            DrawLines(lines, graphicBrush);
        }

        private void DrawAxis()
        {
            List<double[]> lines = new List<double[]>();
            lines.Add(new double[] { marginX, y0, width - marginX, y0 });
            lines.Add(new double[] { x0, marginY, x0, height - marginY });
            lines.Add(new double[] { x0, marginY, x0-arrayLength, marginY+arrayLength});
            lines.Add(new double[] { x0, marginY, x0+arrayLength, marginY+arrayLength});
            lines.Add(new double[] { width-marginX, y0, width-marginX-arrayLength, y0-arrayLength });
            lines.Add(new double[] { width - marginX, y0, width - marginX - arrayLength, y0 + arrayLength });
            DrawLines(lines, axisBrush);
            Label label = new Label();
            label.Content = "x";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Center;
            canvas.Children.Add(label);
        }

        private int countZeros(double a)
        {
            string step = a.ToString();
            step = step.Substring(2);
            int i = 0;
            while (i < step.Length && step[i] == '0')
                i++;
            return i;
        }

        private void findAxisSteps()
        {
            double w = Math.Abs(width-2*marginX);
            double h = Math.Abs(height-2*marginY);
            w /= 10;
            h /= 8;
            //w *= stepX;
            //h *= stepY;
            int n;
            if (w > 1)
                n = 0;
            else n = countZeros(w);
            axisStepX = Math.Round(w, n);

            if (h > 1)
                n = 0;
            else n = countZeros(h);
            axisStepY = Math.Round(h, n);
                
        }

        private double findLength(double a, double b)
        {
            double c = a * b;
            a = Math.Abs(a);
            b = Math.Abs(b);
            if (c < 0)
            {
                return a + b;
            }
            else
            {
                return Math.Max(a, b);
            }
        }

        private void CalculateValues()
        {         
            width = canvas.ActualWidth;
            height = canvas.ActualHeight;
            stepX = (width - marginX * 4) / findLength(minX, maxX);
            stepY = (height - marginY * 4) /findLength(minY, maxY);
            x0 = marginX * 2;
            if (x[0] < 0)
                x0 += Math.Abs(x[0]) * stepX;
            y0 = marginY * 2;
            if (maxY > 0)
                y0 += maxY * stepY;
        }

        private void Paint()
        {
            if (x != null && y != null)
            {
                canvas.Children.Clear();
                CalculateValues();
                DrawAxis();
                if (toDrawNet)
                    DrawNet();
                DrawGraphic(calculatePoints());
            }
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
                Paint();
        }

        private void InputClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                drawMenuButton.IsEnabled = true;
                Points points = new Points(fileDialog.FileName);
                x = points.getX();
                y = points.getY();
                minY = points.getMinY();
                maxY = points.getMaxY();
                minX = x[0];
                maxX = x[x.Length - 1];
            }
        }

        private void ColorClick(object sender, RoutedEventArgs e)
        {
            ColorWindow colorWindow = new ColorWindow();
            colorWindow.ShowDialog();
            if (colorWindow.DialogResult == true)
            {
                graphicBrush.Color = colorWindow.coulor;
            };
        }

        private void DrawClick(object sender, RoutedEventArgs e)
        {
            Paint();  
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void CanvasDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2 && x!=null && y!=null)
            {
                toDrawNet = !toDrawNet;
                Paint();
            }
        }
    }
}
