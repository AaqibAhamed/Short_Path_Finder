using ShortestPath.Core.Entities;
using ShortestPath.Core.Repositories;

namespace ShortestPath.Console;

class Program
{
    static void Main(string[] args)
    {
        IShortPathRepository shortPathRepository = new ShortPathRepository();

        System.Console.WriteLine("WelCome To Shortest Path Finder");

        var graph = GraphBuilder.BuildGraphFromUserInput(); // Build the graph with user input

        System.Console.Write("Enter FROM node: ");
        var fromNode = System.Console.ReadLine()?.Trim() ?? string.Empty;

        System.Console.Write("Enter TO node: ");
        var toNode = System.Console.ReadLine()?.Trim() ?? string.Empty;

        var result = shortPathRepository.FindShortestPath(fromNode, toNode, graph);

        if (result == null)
        {
            System.Console.WriteLine($"No path exists between {fromNode} and {toNode}.");
            return;
        }
        else
        {
            System.Console.WriteLine($"Shortest path from {fromNode} to {toNode}: {string.Join(" -> ", result.NodeNames)}"); // A -> B -> C

            //System.Console.WriteLine($"Shortest path from {fromNode} to {toNode}: {string.Join(" , ", result.NodeNames)}"); A,B,C
        
            System.Console.WriteLine($"Total Distance: {result.Distance}");
        }

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