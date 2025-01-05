using ShortestPath.Core.Entities;

namespace ShortestPath.Console;

public class GraphPrinter
{
    public static void PrintGraph(List<Node> graph)
    {
        System.Console.WriteLine("Generated Graph:");
        foreach (var node in graph)
        {
            System.Console.Write($"Node {node.Name}: ");
            if (node.Edges.Any())
            {
                foreach (var edge in node.Edges)
                {
                    System.Console.Write($"-> {edge.TargetNode.Name} (Weight: {edge.Weight}) ");
                }
            }
            else
            {
                System.Console.Write("No edges");
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
    }

    // public static void PrintGraph2(List<Node> graph)
    // {
    //     System.Console.WriteLine("Graph:");
    //     foreach (var node in graph)
    //     {
    //         System.Console.Write($"{node.Name} -> ");
    //         foreach (var edge in node.Edges)
    //         {
    //             System.Console.Write($"{edge.TargetNode.Name} ({edge.Weight}), ");
    //         }
    //         System.Console.WriteLine();
    //     }
    // }
}