namespace MyDST;
public class Programm
{
    public static int[,] Data = { { 0, 1,1 }, { 1, 0,1 },{ 1, 1,0 } };
    public static int[,] Dta = { 
        { 0, 1, -1 },
        { 1, 0, 1 }, 
        { -1, 1, 0 } 
    };
    static void Main(string[] args)
    {
        var g = new Graph(Data, 0);


       foreach(var x in g.Arcs)
        {
            Console.WriteLine(x.ToString());
        }
        g = new Graph(Dta, 1);
        Console.WriteLine("Warning!");
        foreach (var x in g.Arcs)
        {
            Console.WriteLine(x.ToString());
        }
    }
}