using laba_aisd_1;

var graph = new Graph("graph.txt");

graph.Print();

var ostov = graph.Prim();

foreach (var pair in ostov)
{
    Console.WriteLine($"{pair}");
}

graph.DoDfs();