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
        private bool numbersAreAlreadyCalculated;
        private List<double[]> xNetLines;
        private List<double[]> yNetLines;
        private string[] xValues;
        private string[] yValues;

        private SolidColorBrush graphicBrush = new SolidColorBrush(Color.FromRgb(100, 200, 34));


        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            width = 0;
            height = 0;
            marginX = 0;
            marginY = 0;
            stepX = 0;
            stepY = 0;
            minX = 0;
            minY = 0;
            maxX = 0;
            maxY = 0;
            x0 = 0;
            y0 = 0;
            arrayLength = 0;
            axisStepX = 0;
            axisStepY = 0;
            toDrawNet = false;

            graphicBrush.Color = Color.FromRgb((byte)Application.Current.Resources["R"], (byte)Application.Current.Resources["G"], (byte)Application.Current.Resources["B"]);
            drawMenuButton.IsEnabled = false;
            marginX = (double)this.Resources["MARGIN_X"];
            marginY = (double)this.Resources["MARGIN_Y"];
            arrayLength = (double)this.Resources["ARROW_LENGTH"];
            numbersAreAlreadyCalculated = false;
            xValues = new string[12];
            yValues = new string[10];
        }

        private void DrawGraphic(PointCollection polylinePoints)
        {
            Polyline polyline = new Polyline();
            polyline.Stroke = graphicBrush;
            polyline.Points = polylinePoints;
            polyline.StrokeThickness = 3;
            canvas.Children.Add(polyline);
        }

        private void DrawLines(List<double[]> lines, Style style)
        {
            foreach (double[] points in lines)
            {
                Line line = new Line();
                line.Style = style;
                line.X1 = points[0];
                line.Y1 = points[1];
                line.X2 = points[2];
                line.Y2 = points[3];
                canvas.Children.Add(line);

            }
        }

        private double calculateNumber(bool axis, double p0, double p, double step)
        {
            double buf;
            if (axis)
                buf = p - p0;
            else
                buf = p0 - p;
            return buf /= step;
        }

        private void calculateNet()
        {
            xNetLines = new List<double[]>();
            double axisStepX = (width - 2 * marginX) / 11;
            double axisStepY = (height - 2 * marginY) / 9;
            double cur = x0;
            while (cur <= width - marginX)
            {
                xNetLines.Add(new double[] { cur, marginY, cur, height - marginY });
                cur += axisStepX;
            }

            cur = x0;
            while (cur >= marginX)
            {
                xNetLines.Add(new double[] { cur, marginY, cur, height - marginY });
                cur -= axisStepX;
            }

            yNetLines = new List<double[]>();
            cur = y0;
            while (cur <= height - marginY)
            {
                yNetLines.Add(new double[] { marginX, cur, width - marginX, cur });
                cur += axisStepY;
            }

            cur = y0;
            while (cur >= marginY)
            {
                yNetLines.Add(new double[] { marginX, cur, width - marginX, cur });
                cur -= axisStepY;
            }

            if (!numbersAreAlreadyCalculated)
            {
                int i = 0;
                foreach (double[] x in xNetLines)
                {
                    xValues[i] = string.Format("{0:F2}", calculateNumber(true, x0, x[0], stepX));
                    i++;
                }

                i = 0;
                foreach (double[] y in yNetLines)
                {
                    yValues[i] = string.Format("{0:F2}", calculateNumber(false, y0, y[1], stepY));
                    i++;
                }
                numbersAreAlreadyCalculated = true;
            }
        }



        private void DrawNumbers()
        {
            double left;
            double top;
            top = y0;
            int j = 0;
            Label label1;
            Canvas can;
            foreach (double[] i in xNetLines)
            {
                left = i[0];
                label1 = new Label();
                label1.Style = (Style)this.Resources["LABEL_STYLE"];
                label1.Content = xValues[j];
                Canvas.SetTop(label1, top);
                Canvas.SetLeft(label1, left - 15);
                can = new Canvas();
                can.Children.Add(label1);
                canvas.Children.Add(can);
                j++;
            }

            left = x0;
            j = 0;
            foreach (double[] i in yNetLines)
            {
                top = i[1];
                label1 = new Label();
                label1.Style = (Style)this.Resources["LABEL_STYLE"];
                label1.Content = yValues[j];
                Canvas.SetTop(label1, top - 10);
                Canvas.SetLeft(label1, left);
                can = new Canvas();
                can.Children.Add(label1);
                canvas.Children.Add(can);
                j++;
            }

            label1 = new Label();
            label1.Style = (Style)this.Resources["XY_STYLE"];
            label1.Content = "x";
            Canvas.SetTop(label1, y0);
            Canvas.SetLeft(label1, width - marginX * 2);
            can = new Canvas();
            can.Children.Add(label1);
            canvas.Children.Add(can);

            label1 = new Label();
            label1.Style = (Style)this.Resources["XY_STYLE"];
            label1.Content = "y";
            Canvas.SetTop(label1, marginY - 10);
            Canvas.SetLeft(label1, x0 + (double)this.Resources["ARROW_LENGTH"]);
            can = new Canvas();
            can.Children.Add(label1);
            canvas.Children.Add(can);
        }

        private void DrawNet()
        {
            DrawLines(xNetLines, (Style)this.Resources["NET_STYLE"]);
            DrawLines(yNetLines, (Style)this.Resources["NET_STYLE"]);

        }

        private void DrawAxis()
        {
            List<double[]> lines = new List<double[]>();
            lines.Add(new double[] { marginX, y0, width - marginX, y0 });
            lines.Add(new double[] { x0, marginY, x0, height - marginY });
            lines.Add(new double[] { x0, marginY, x0 - arrayLength, marginY + arrayLength });
            lines.Add(new double[] { x0, marginY, x0 + arrayLength, marginY + arrayLength });
            lines.Add(new double[] { width - marginX, y0, width - marginX - arrayLength, y0 - arrayLength });
            lines.Add(new double[] { width - marginX, y0, width - marginX - arrayLength, y0 + arrayLength });
            DrawLines(lines, (Style)this.Resources["AXIS_STYLE"]);
            calculateNet();
            DrawNumbers();
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
            stepY = (height - marginY * 4) / findLength(minY, maxY);
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
                Init();
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
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2 && x != null && y != null)
            {
                toDrawNet = !toDrawNet;
                Paint();
            }
        }
    }
}
