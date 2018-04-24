using System;
using System.Linq;

namespace Graph
{

  class Program
  {
    static void Main(string[] args)
    {
      var I = new [] { 0, 0, 1, 2 };
      var J = new [] { 1, 2, 3, 3 };
      var C = new [] { 3, 4, 6, 4 };

      //var I = new int[] {1, 1, 2};
      //var J = new int[] {2, 3, 3};
      //var C = new int[] {3, 10, 4};

      var graph = new DGraph(I, J, C);

      Console.WriteLine(graph.GetMinArc(1));
      Console.WriteLine();
      graph.PrintHL();
    }
  }
}
