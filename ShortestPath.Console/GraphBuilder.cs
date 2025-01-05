using ShortestPath.Core.Entities;

namespace ShortestPath.Console;

public class GraphBuilder
{
   public static List<Node> BuildGraphFromUserInput()
   {
      System.Console.WriteLine("Building a graph based on user input...");

      // Step 1: Ask for the number of nodes
      System.Console.Write("Enter the number of nodes in the graph: ");
      int nodeCount;
      while (!int.TryParse(System.Console.ReadLine(), out nodeCount) || nodeCount <= 0)
      {
         System.Console.Write("Invalid input. Please enter a positive integer for the number of nodes: ");
      }

      var graph = new List<Node>();

      // Step 2: Add nodes to the graph
      for (int i = 0; i < nodeCount; i++)
      {
         System.Console.Write($"Enter the name for node {i + 1}: ");
         string nodeName = System.Console.ReadLine()?.Trim() ?? string.Empty;

         if (string.IsNullOrWhiteSpace(nodeName))
         {
            System.Console.WriteLine("Node name cannot be empty. Please try again.");
            i--; // Retry the same node
            continue;
         }

         graph.Add(new Node { Name = nodeName, Edges = new List<Edge>() });
      }

      // Step 3: Add edges for each node
      foreach (var node in graph)
      {
         System.Console.Write($"How many edges does node '{node.Name}' have? ");
         int edgeCount;
         while (!int.TryParse(System.Console.ReadLine(), out edgeCount) || edgeCount < 0)
         {
            System.Console.Write("Invalid input. Please enter a non-negative integer for the number of edges: ");
         }

         for (int j = 0; j < edgeCount; j++)
         {
            System.Console.Write($"Enter the target node for edge {j + 1} of node '{node.Name}': ");

            var targetNode = System.Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(targetNode) || graph.All(n => n.Name != targetNode))
            {
               System.Console.WriteLine($"Target node '{targetNode}' is invalid or does not exist in the graph. Please try again.");
               j--; // Retry the same edge
               continue;
            }

            System.Console.Write($"Enter the weight for the edge from '{node.Name}' to '{targetNode}': ");
            int weight;
            while (!int.TryParse(System.Console.ReadLine(), out weight) || weight <= 0)
            {
               System.Console.Write("Invalid input. Please enter a positive integer for the weight: ");
            }

            // Add edge to the node
            var targetNodeObject = graph.First(n => n.Name == targetNode);
            node.Edges.Add(new Edge { TargetNode = targetNodeObject, Weight = weight });
         }
      }

      System.Console.WriteLine("Graph construction completed.");
      return graph;
   }

}