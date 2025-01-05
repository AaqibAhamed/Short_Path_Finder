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

        GraphPrinter.PrintGraph(graph); // Print the graph

        System.Console.Write("Enter FROM node: ");
        var fromNode = System.Console.ReadLine()?.Trim() ?? string.Empty;

        System.Console.Write("Enter TO node: ");
        var toNode = System.Console.ReadLine()?.Trim() ?? string.Empty;


        System.Console.WriteLine($"From Node: {fromNode}, To Node: {toNode}");

        // Debugging Nodes
        // System.Console.WriteLine("Graph Nodes:");
        
        // foreach (var node in graph)
        // {
        //     System.Console.WriteLine($"Node: {node.Name}, Edges: {string.Join(", ", node.Edges.Select(e => $"{e.TargetNode.Name} ({e.Weight})"))}");
        // }

        var result = shortPathRepository.FindShortestPath(fromNode, toNode, graph);

        if (result == null)
        {
            System.Console.WriteLine($"No path exists between {fromNode} and {toNode}.");
            return;
        }
        else
        {
            //System.Console.WriteLine($"Shortest path from {fromNode} to {toNode}: {string.Join(" -> ", result.NodeNames)}"); // A -> B -> C

            System.Console.WriteLine($"Shortest path from NodeName = \"{fromNode}\" , to NodeName = \"{toNode}\" : {string.Join(" , ", result.NodeNames)}"); // A,B,C - requested in document

            System.Console.WriteLine($"Total Distance: {result.Distance}");
        }

    }

}