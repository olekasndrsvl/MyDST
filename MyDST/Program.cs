namespace MyDST;
public class Programm
{
    public static int[,] Data = { { 0, 1,1 }, { 1, 0,1 },{ 1, 1,0 } };
    public static int[,] Dta = { 
        { -2, 0, 0, -5,-19 },
        { 2, -1, 0, 0,0 }, 
        { 0, 1, -1,5, 0},
        { 0, 0, 1, 0,19}
    };
    static void Main(string[] args)
    {
        var g = new Graph(Data, 0);


       foreach(var x in g.Arcs)
        {
            Console.WriteLine(x.ToString());
        }
        g = new Graph(Dta, 1);
        g.Dijkstra(g.Peaks.First());
        Console.WriteLine("Warning!");
        foreach (var x in g.Arcs)
        {
            Console.WriteLine(x.ToString());
        }
        foreach (var x in g.Peaks)
        {
            Console.WriteLine(x.count);
        }

        Console.WriteLine("test dst");
        var t = Graph.DST(g.Peaks.First()).ToList();
        
        foreach (var x in t)
        {
            Console.WriteLine(x);
        }
        
    }
}