using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
  class DGraph
  {
    private int[] I; //Начала дуг
    private int[] J; //Концы дуг
    private int[] C; //величины расстояний до исходной вершины.

    private int[] H; //Головы списков
    private int[] L; //Ссылки элементов списков друг на друга

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="I">Массив начал дуг</param>
    /// <param name="J">Массив концов дуг</param>
    /// <param name="C">Масив значений дуг</param>
    public DGraph(int[] I, int[] J, int[] C)
    {

      this.I = I;
      this.J = J;
      this.C = C;

      //H = new int[(I.Max() + 1)];
      H = new int[(I.Concat(J).Max() + 1)];
      H = Enumerable.Repeat(-1, H.Length).ToArray();
      L = new int[I.Length];

      this.FillHL();
    }

    /// <summary>
    /// Инициализация списков. В начале все списки пусты
    /// </summary>
    private void FillHL()
    {
      for (int k = 0; k < I.Length; k++) // Обход дуг сети
      {
        int i = I[k]; // начальная вершина дуги
                      // добавляем дугу k в начало списка i
        L[k] = H[i]; // k ссылается на дугу, которая раньше была первой в списке i
        H[i] = k;    // k становится первой дугой в списке i
      } // списки пучков дуг построены
    }
    /// <summary>
    /// Поиск минимальной дуги из вершины i
    /// </summary>
    /// <param name="Vertex">Вершина</param>
    /// <returns></returns>
    public int GetMinArc(int Vertex)
    {
      int minArc = H[Vertex];
      // Просмотр пучка дуг, выходящих из вершины i
      for (int k = H[Vertex]; k != -1; k = L[k])
      {
        // просматриваем k-ю дугу
        if (C[minArc] > C[k]) minArc = k;
      }
      return minArc;
    }
    /// <summary>
    /// Наибольшая вершина
    /// </summary>
    /// <param name="Vertex"></param>
    /// <returns></returns>
    public int GetMaxArc(int Vertex)
    {
      int minArc = H[Vertex];
      // Просмотр пучка дуг, выходящих из вершины i
      for (int k = H[Vertex]; k != -1; k = L[k])
      {
        // просматриваем k-ю дугу
        if (C[minArc] < C[k]) minArc = k;
      }
      return minArc;
    }
    /// <summary>
    /// Удаление вершины из списков HL
    /// </summary>
    /// <param name="Vertex">Вершина из которой удаляем</param>
    /// <param name="Arc">Дуга которую удаляем</param>
    public void Delete(int Vertex, int Arc)
    {
      if (H[Vertex] == Arc) //Если дуга в H
      {
        int temp = H[Vertex];
        H[Vertex] = L[H[Vertex]];
        L[temp] = -1;
      }
      else
      {
        for (int k = H[Vertex]; k != -1; k = L[k])
        {
          if (L[k] == Arc)
          {
            int temp = L[k];
            L[k] = L[L[k]];
            L[temp] = -1;
          }
        }
      }
    }
    /// <summary>
    /// Перемещение дуги в начало списка, т.е в H
    /// </summary>
    /// <param name="Vertex">Вершина в которой перемещаем</param>
    /// <param name="Arc">Дуга которую перемещаем</param>
    public void MoveToBegin(int Vertex, int Arc)
    {
      if (H[Vertex] != Arc)
      {
        for (int k = H[Vertex]; k != -1; k = L[k])
        {
          if (L[k] == Arc)
          {
            int temp = L[k];
            L[k] = L[L[k]];
            L[temp] = H[Vertex];
            H[Vertex] = temp;
          }
        }
      }
    }
    /// <summary>
    /// Перемещение дуги в конец списка (указатель на -1)
    /// </summary>
    /// <param name="Vertex">Вершина в которой перемещаем</param>
    /// <param name="Arc">Дуга которую перемещаем</param>
    public void MoveToEnd(int Vertex, int Arc)
    {
      if (H[Vertex] == Arc) //Если вершина в начале
      {
        if (L[H[Vertex]] != -1) //Если дуга не является единственной
        {
          int minusOneIndex = 0;
          for (int k = H[Vertex]; k != -1; k = L[k]) //Ищем L с последней дугой всписке
          {
            if (L[k] == -1)
            {
              minusOneIndex = k;
              break;
            }
          }
          int temp = L[H[Vertex]];
          L[H[Vertex]] = -1;
          L[minusOneIndex] = H[Vertex];
          H[Vertex] = temp;
        }
      }
      else
      {
        if (L[Arc] != -1) //Если вершина Arc не в конце (не ссылается на -1)
        {
          int minusOneIndex = 0;
          for (int k = H[Vertex]; k != -1; k = L[k]) //Ищем L с последней дугой всписке
          {
            if (L[k] == -1)
            {
              minusOneIndex = k;
              break;
            }
          }
          for (int k = H[Vertex]; k != -1; k = L[k])
          {
            if (L[k] == Arc)
            {
              int temp = L[L[k]];
              L[minusOneIndex] = Arc;
              L[L[k]] = -1;
              L[k] = temp;
              break;
            }
          }
        }
      }
    }
    /// <summary>
    /// Вычислене среднего значения дуг несчитая EArc
    /// </summary>
    /// <param name="Vertex">Вершина дуги которой считаем</param>
    /// <param name="EArc">Игнорируемая дуга</param>
    /// <returns></returns>
    public double AverageAcrCExeptEArc(int Vertex, int EArc)
    {
      double result = 0;
      int i = 0;
      for (int k = H[Vertex]; k != -1; k = L[k])
      {
        if (EArc != k)
        {
          result += C[k];
          i++;
        }
      }
      result /= i;
      return result;
    }
    /// <summary>
    /// Вывод на экран всех пучков дуг
    /// </summary>
    public void PrintHL()
    {
      Console.WriteLine("+-+-----------------------------------------+");
      Console.WriteLine("|N|: H -> L                                 |");
      Console.WriteLine("+-+-----------------------------------------+");
      for (int iVertex = 0; iVertex < H.Length; iVertex++)
      {
        Console.Write("|{0}|: ", iVertex);
        Console.Write("{0}", H[iVertex]);
        for (int k = H[iVertex]; k != -1; k = L[k])
        {
          Console.Write(" -> {0}", L[k]);
        }
        Console.WriteLine();
      }
      Console.WriteLine("+-+-----------------------------------------+");
    }

  }
}
