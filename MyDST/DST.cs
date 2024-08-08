using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDST
{
    public class Peak // класс вершины графа
    {
        public string Name { get; set; } // 
        /// <summary>
        /// При применении алгоритма Дейкстры в данное поле будет записана длина кратчайшего пути из стартовой вершины в данную.
        /// </summary>
        public double count = -1; // для Дейкстры
        public List<Peak> IncidnetialPeaks;
        public Peak()
        {
            IncidnetialPeaks = new List<Peak>();
        }
        public Peak(string name)
        {
            Name = name;
            IncidnetialPeaks = new List<Peak>();
        }
        public override string ToString()
        {
            return $"| {Name} Peaks:{string.Join(" ", IncidnetialPeaks.Select(x=>x.Name))} |";
        }
    }
    /// <summary>
    /// Класс дуги графа
    /// </summary>
    public class Arc
    {
        public double Weight = 0;
        public Peak StartPeak;
        public Peak EndPeak;
        public bool IsLocated;
        public Arc(Peak startPeak, Peak endPeak, bool islocated=true) // для не взешенного графа
        {
            StartPeak = startPeak;
            EndPeak = endPeak;
            IsLocated = islocated;
        }
        public Arc(Peak startPeak, Peak endPeak, double weight, bool islocated=true) // для взешенного графа
        {
            StartPeak = startPeak;
            EndPeak = endPeak;
            Weight = weight;
            IsLocated = islocated;
        }
        public int CompareTo(Arc other)
        {
            if (other == null) return 1;
            return Weight.CompareTo(other.Weight);
        }
        public override string ToString()
        {
            return $"Weigth: {Weight} Located: {IsLocated} Peaks:{StartPeak.Name}  {EndPeak.Name}";

        }


    }
    /// <summary>
    /// Класс графа
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Все вершины графа
        /// </summary>
        public IEnumerable<Peak>? Peaks { get; set; }
        /// <summary>
        /// Все дуги графа
        /// </summary>
        public IEnumerable<Arc>? Arcs { get; set; }

        /// <summary>
        /// Матрица смежности графа
        /// </summary>
        public int[,] AdjacencyMatrix;
        /// <summary>
        /// Матрица инцидентности графа
        /// </summary>
        public int[,] IncidenceMatrix;



        /// <summary>
        /// Алгоритм поиска Краскаловского дерева(Остовного дерева графа). Возвращает IEnumerable коллекцию дуг остовного дерева.
        /// </summary>
        /// <param name="startPeak"></param>
        /// <returns></returns>
        public static IEnumerable<Arc> DST(Peak startPeak)
        {
            List<Peak> visitedPeaks = new List<Peak> ();
            visitedPeaks.Add (startPeak);
            List<Arc> result = new List<Arc> ();

            // Рекурсивный обход графа.
            void dst(Peak peak)
            {
                var tmp = peak.IncidnetialPeaks;
                foreach(var x in tmp)
                {
                    Console.WriteLine(x);
                    if(!visitedPeaks.Contains(x))
                    {
                        visitedPeaks.Add(x);
                        result.Add(new Arc(peak, x, true));
                        dst(x);

                      
                    }
                }



            }

            dst(startPeak);
            return result;






        }




        public void Dijkstra(Peak start)
        {
            start.count = 0;

            void d_algorithm(Peak peak)
            {
                foreach(var x in Arcs)
                {
                    if(x.StartPeak == peak)
                    {
                        if (x.EndPeak.count != -1)
                        {
                            x.EndPeak.count= Math.Min(x.EndPeak.count, x.StartPeak.count+x.Weight);
                        }
                        else
                        {
                            x.EndPeak.count = x.StartPeak.count+ x.Weight;
                        }
                    }
                }

                foreach (var x in peak.IncidnetialPeaks)
                {
                    d_algorithm(x);
                }



            }

            d_algorithm(start);









        }






        /// <summary>
        /// Конструктор создает граф заданный матрицей смежности/инцедентости.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="typeofmatrix"></param>
        public Graph(int[,] matrix, int typeofmatrix)
        {
            var peaks = new List<Peak>();
            var arcs = new List<Arc>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                peaks.Add(new Peak($"{i}"));
            }

            switch (typeofmatrix)
            {
                case 0:
                    if(matrix.GetLength(0) != matrix.GetLength(1))
                    {
                        throw new ArgumentException("It is not adjency matrix!");
                    }
                    

                    
                    
                   

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        var selectedPeak = peaks[i];
                        for (int j =0;j< matrix.GetLength(0); j++)
                        {
                            if (matrix[i,j] != 0)
                            {
                                selectedPeak.IncidnetialPeaks.Add(peaks[j]);
                                arcs.Add(new Arc(selectedPeak, peaks[j], matrix[i,j],true));
                            }


                        }
                    }
                    Peaks=peaks;
                    Arcs = arcs;


                    break;

                case 1:
                   
                    for (int j = 0; j < matrix.GetLength(1);j++)
                    {
                        Peak start = null;
                        Peak end = null;
                        int weight = 0;
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            weight = Math.Max(weight, matrix[i, j]);

                            if ((matrix[i, j] ) >0)
                            {
                                end = peaks[i];
                            }
                            if ((matrix[i, j] ) <0 )
                            {
                                start = peaks[i];
                            }
                            if (start != null && end != null)
                            {
                                start.IncidnetialPeaks.Add(end);
                                arcs.Add(new Arc(start, end, weight,true));
                                start = null;
                                end = null;

                            }
                            Console.Write(matrix[i, j]+" #"+i+"  ");
                            

                        }


                        Console.WriteLine();
                    }

                    Peaks = peaks;
                    Arcs = arcs;


                    break;


                default:
                    throw new ArgumentException("Invalid type of matrix!");

            }



            
        }
       


    }




}
