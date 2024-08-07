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
        protected int count; // для Дейкстры
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

    public class Arc // класс дуги графа
    {
        public double Weight = 0;
        public Peak StartPeak;
        public Peak EndPeak;
        public bool IsLocated;
        public Arc(Peak startPeak, Peak endPeak, bool islocated) // для не взешенного графа
        {
            StartPeak = startPeak;
            EndPeak = endPeak;
            IsLocated = islocated;
        }
        public Arc(Peak startPeak, Peak endPeak, double weight, bool islocated) // для взешенного графа
        {
            StartPeak = startPeak;
            EndPeak = endPeak;
            Weight = weight;
            IsLocated = islocated;
        }

        public override string ToString()
        {
            return $"Weigth: {Weight} Located: {IsLocated} Peaks:{StartPeak}  {EndPeak}";

        }


    }
    /// <summary>
    /// Класс графа
    /// </summary>
    public class Graph
    {
        public IEnumerable<Peak>? Peaks { get; set; }
        public IEnumerable<Arc>? Arcs { get; set; }

        public int[,] AdjacencyMatrix;
        public int[,] IncidenceMatrix;

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
                                arcs.Add(new Arc(selectedPeak, peaks[j],true));
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
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            

                            if (matrix[i,j] == 1)
                            {
                                end = peaks[i];
                            }
                            if (matrix[i, j] == -1)
                            {
                                start = peaks[i];
                            }
                            if (start != null && end != null)
                            {
                                start.IncidnetialPeaks.Add(end);
                                arcs.Add(new Arc(start, end, true));

                            }

                        }
                        


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
