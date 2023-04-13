/*
using System;


class CubicSpline
{
    static void Main()
    {
        // вписуємо функцію і вводимо значення ікс
        Func<double, double> f = x => Math.Pow(x, 4) - 2 * Math.Pow(x, 3) + Math.Pow(x, 2) - 2 * x + 1;
        double[] x = new double[] { -1, 0, 1, 2 };

        // рахуємо значення ігрик
        double[] y = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            y[i] = f(x[i]);
        }

        int n = x.Length;
        double[] h = new double[n - 1];
        double[] alpha = new double[n - 1];
        double[] l = new double[n];
        double[] u = new double[n];
        double[] z = new double[n];

        // Step 1
        for (int i = 0; i < n - 1; i++)
        {
            h[i] = x[i + 1] - x[i];
        }

        // Step 2
        for (int i = 1; i < n - 1; i++)
        {
            alpha[i] = 3.0 / h[i] * (y[i + 1] - y[i]) - 3.0 / h[i - 1] * (y[i] - y[i - 1]);
        }

        // Step 3
        l[0] = 1.0;
        u[0] = 0.0;
        z[0] = 0.0;

        // Step 4
        for (int i = 1; i < n - 1; i++)
        {
            l[i] = 2.0 * (x[i + 1] - x[i - 1]) - h[i - 1] * u[i - 1];
            u[i] = h[i] / l[i];
            z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
        }
        // Step 5
        l[n - 1] = 1.0;
        z[n - 1] = 0.0;
        double[] c = new double[n];
        double[] b = new double[n - 1];
        double[] d = new double[n - 1];

        // Step 6
        for (int j = n - 2; j >= 0; j--)
        {
            c[j] = z[j] - u[j] * c[j + 1];
            b[j] = (y[j + 1] - y[j]) / h[j] - h[j] * (c[j + 1] + 2.0 * c[j]) / 3.0;
            d[j] = (c[j + 1] - c[j]) / (3.0 * h[j]);
        }

        // Output
        for (int i = 0; i < n - 1; i++)
        {
            Console.WriteLine("Intervaly[{0}, {1}]   {2:f6} + {3:f6}(x - {4:f6}) +{5:f6}(x - {4:f6})^2 + {6:f6}(x - {4:f6})^3",
                x[i], x[i + 1], y[i], b[i], x[i], c[i], d[i]);
        }
    }
}
*/

public class Maaaaaaaaaaaaain
{
    public static double[] GetX(int n, double a, double b)
    {
        double[] x = new double[n];
        double h = (b - a) / (n - 1);
        for (int i = 0; i < n; i++)
        {
            x[i] = a + i * h;
        }
        return x;
    }
    public static double[] GetY(double[] x)
    {
        double[] y = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            y[i] = Math.Pow(x[i], 4) - 2 * Math.Pow(x[i], 3) + Math.Pow(x[i], 2) - 2 * x[i] + 1;
        }
        return y;
    }
    private double[] x;
    private double[] y;
    private double[] a;
    private double[] b;
    private double[] c;
    private double[] d;
    public Maaaaaaaaaaaaain(double[] x, double[] y)
    {
        this.x = x;
        this.y = y;
        this.a = y;
        this.b = new double[x.Length];
        this.c = new double[x.Length];
        this.d = new double[x.Length];
        computeCoefficients();
    }

    private void computeCoefficients()
    {
        int n = x.Length;
        double[] h = new double[n - 1];
        double[] alpha = new double[n - 1];
        double[] l = new double[n];
        double[] mu = new double[n];
        double[] z = new double[n];

        for (int i = 0; i < n - 1; i++)
        {
            h[i] = x[i + 1] - x[i];
        }

        for (int i = 1; i < n - 1; i++)
        {
            alpha[i] = 6 * ((a[i + 1] - a[i]) / h[i] - (a[i] - a[i - 1]) / h[i - 1]);
        }

        l[0] = 1;
        mu[0] = 0;
        z[0] = 0;

        for (int i = 1; i < n - 1; i++)
        {
            l[i] = 2 * (x[i + 1] - x[i - 1]) - h[i - 1] * mu[i - 1];
            mu[i] = h[i] / l[i];
            z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
        }

        l[n - 1] = 1;
        z[n - 1] = 0;
        c[n - 1] = 0;

        for (int j = n - 2; j >= 0; j--)
        {
            c[j] = z[j] - mu[j] * c[j + 1];
            d[j] = (c[j + 1] - c[j]) / (h[j]);
        }
        for (int i = 1; i < b.Length; i++)
        {
            b[i] = c[i] * h[i - 1] / 2 - Math.Pow(h[i - 1], 2) * d[i - 1] / 6 + (a[i] - a[i - 1]) / h[i - 1];
        }
    }
    public void Print()
    {
        for (int i = 0; i < x.Length - 1; i++)
        {
            Console.WriteLine("Intervaly[" + x[i] + ";" + x[i + 1] + "]  " + y[i + 1] + " + " + b[i + 1] + "(x - " + x[i + 1] + ") +  " + c[i + 1] / 2 + "(x - " + x[i + 1] + ")^2 + " + d[i] / 6 + "(x - " + x[i + 1] + ")^3");
        }
    }
    public static void Main(string[] args)
    {
        int n = 4;
        var x = GetX(n, -1, 2);
        var y = GetY(x);
        for (int i = 0; i < x.Length; i++)
        {
            Console.WriteLine("x[" + i + "]= " + x[i]);
        }
        Console.WriteLine("-----------------------------------------------");
        for (int i = 0; i < y.Length; i++)
        {
            Console.WriteLine("y[" + i + "]= " + y[i]);
        }
        Maaaaaaaaaaaaain cubicSpline = new Maaaaaaaaaaaaain(x, y);
        cubicSpline.Print();
    }
}

