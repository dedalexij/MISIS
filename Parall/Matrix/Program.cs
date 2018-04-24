using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix
{
  class Program
  {
    static double[,] Mult(double[,] a, double[,] b, int c)
    {
      double[,] ans = new double[c, c];

      Parallel.For(0, c, i =>
        {
          for (int j = 0; j < c; j++)
          {
            for (int k = 0; k < c; k++)
            {
              ans[i, j] += a[i, k] * b[k, j];
            }
          }
        }
      );



      return ans;
    }

    static void Main(string[] args)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
      var lines = File.ReadAllLines("matrix.csv");
      var matrix = new double[lines.Length, lines.Length];
      var i = 0;
      foreach (string line in lines)
      {
        string[] elements = line.Split(',');
        int j = 0;
        foreach (string str in elements)
        {
          matrix[i, j] = Double.Parse(str, CultureInfo.InvariantCulture);
          j++;
        }
        i++;
      }

      Console.WriteLine(matrix[1, 2]);

      matrix = Mult(matrix, matrix, lines.Length);

      var stringArr = new string[lines.Length];

      for (int j = 0; j < lines.Length; j++)
      {
        var matrValue = matrix[0, 0].ToString();
        for (int k = 1; k < lines.Length; k++)
        {
          matrValue += "," + matrix[j, k];
        }

        stringArr[j] = matrValue;
      }

      File.WriteAllLines("colomn.csv", stringArr);

      Console.WriteLine(matrix[1, 2]);



      Console.ReadKey();
    }
  }
}
