using Newtonsoft.Json;
using Reflection;
using System.Diagnostics;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var sw = new Stopwatch();
        var f = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
        var serializer = new SerializerCSV();
        sw.Start();
        var res = "";
        for (int i = 0; i < 100000; i++)
        {
            res = serializer.Serialize(f, ';');
        }
        sw.Stop();
        Console.WriteLine($"csv serializer {sw.Elapsed.Milliseconds}");
        sw.Restart();
        sw.Start();
        for (int i = 0; i < 100000; i++)
        {
            var d = serializer.Deserialize<F>(res, ';');
        }
        sw.Stop();
        Console.WriteLine($"deserialize {sw.Elapsed.Milliseconds}");
        sw.Restart();
        sw.Start();
        Console.WriteLine(res);
        sw.Stop();
        Console.WriteLine($"console writing {sw.Elapsed.Milliseconds}");
        sw.Restart();
        sw.Start();
        for(int i = 0; i < 100000; i++)
        {
            res = JsonConvert.SerializeObject(f);
        }
        sw.Stop();
        Console.WriteLine($"Json serializer {res}");
        Console.WriteLine($"json serializer time: {sw.Elapsed.Milliseconds}");

        
    }
}