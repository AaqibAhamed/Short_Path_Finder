using ShortestPath.Core;
using ShortestPath.Core.Entities;

class Program
{
    static void Main(string[] args)
    {
        IShortPathRepository shortPathRepository = new ShortPathRepository();

        Console.Write("Enter FROM node: ");
        var fromNode = Console.ReadLine();

        Console.Write("Enter TO node: ");
        var toNode = Console.ReadLine();

        var graph = BuildSampleGraph(); // Build the graph with sample data

        var result = shortPathRepository.FindShortestPath(fromNode, toNode, graph);

        Console.WriteLine($"Shortest Path: {string.Join(", ", result.NodeNames)}");
        
        Console.WriteLine($"Total Distance: {result.Distance}");

        Console.WriteLine(BuildSampleGraph);
    }

    static List<Node> BuildSampleGraph()
    {
        // Example graph construction
        var nodeA = new Node { Name = "A" };
        var nodeB = new Node { Name = "B" };
        var nodeC = new Node { Name = "C" };
        var nodeD = new Node { Name = "D" };

        nodeA.Edges.Add(new Edge { TargetNode = nodeB, Weight = 1 });
        nodeB.Edges.Add(new Edge { TargetNode = nodeC, Weight = 2 });
        nodeC.Edges.Add(new Edge { TargetNode = nodeD, Weight = 3 });

        return new List<Node> { nodeA, nodeB, nodeC, nodeD };
    }
}
