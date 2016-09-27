using System;
using System.IO;
using System.Windows;

namespace WpfApp
{
    class Points
    {

        private double[] x = { -10, 0, 10 };
        private double[] y = { -10, 0, 10 };

        private double minY;
        private double maxY;

        private String path;

        public Points()
        {
            process();
        }

        public Points(String path)
        {
            this.path = path;
            process();
        }

        public double[] getX()
        {
            return x;
        }

        public double[] getY()
        {
            return y;
        }

        public double getMinY()
        {
            return minY;
        }

        public double getMaxY()
        {
            return maxY;
        }

        private void process()
        {
            readFile();
            sort();
            findMaxAndMinY();
        }

        private void readFile ()
        {
            try
            {
                StreamReader streamReader = new StreamReader(path);
                string str = "";

                str = streamReader.ReadLine();
                String[] strArrX = str.Split(' ');
                str = streamReader.ReadLine();
                String[] strArrY = str.Split(' ');
                if (strArrX.Length != strArrY.Length)
                    throw new Exception("Wrong input file format!");
                x = new double[strArrX.Length];
                y = new double[strArrY.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] = Convert.ToDouble(strArrX[i]);
                    y[i] = Convert.ToDouble(strArrY[i]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Wrong input file format!", "Oops");
            }
        }

        private void findMaxAndMinY()
        {
            minY = y[0];
            maxY = y[0];
            for (int i=1; i<y.Length; i++)
            {
                if (y[i] > maxY)
                    maxY = y[i];
                if (y[i] < minY)
                {
                    minY = y[i];
                }
            }
        }

        private void sort()
        {
            for (int i=0; i<x.Length-1; i++)
            {
                for (int j=i+1; j<x.Length; j++)
                {
                    if (x[j] < x[i])
                    {
                        double bufX = x[j];
                        double bufY = y[j];
                        x[j] = x[i];
                        y[j] = y[i];
                        x[i] = bufX;
                        y[i] = bufY;
                    }
                }
            }
        }
    }
}
