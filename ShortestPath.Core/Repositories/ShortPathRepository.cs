using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core.Repositories;

public class ShortPathRepository : IShortPathRepository
{
    public ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph)
    {
        var distances = new Dictionary<string, int>();
        var previousNodes = new Dictionary<string, Node>();
        var priorityQueue = new SortedSet<(string Node, int Distance)>
            (Comparer<(string, int)>.Create((a, b) => a.Item2 == b.Item2 ?
            string.Compare(a.Item1, b.Item1, StringComparison.Ordinal) : a.Item2.CompareTo(b.Item2)));

        foreach (var node in graph)
        {
            distances[node.Name] = int.MaxValue;
            priorityQueue.Add((node.Name, int.MaxValue));
        }

        distances[fromNodeName] = 0;
        priorityQueue.Add((fromNodeName, 0));

        while (priorityQueue.Count > 0)
        {
            var (currentNodeName, currentDistance) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentNodeName == toNodeName) break;

            var currentNode = graph.First(n => n.Name == currentNodeName);

            foreach (var edge in currentNode.Edges)
            {
                var neighbor = edge.TargetNode;
                var newDistance = currentDistance + edge.Weight;

                if (newDistance < distances[neighbor.Name])
                {
                    priorityQueue.Remove((neighbor.Name, distances[neighbor.Name]));
                    distances[neighbor.Name] = newDistance;
                    previousNodes[neighbor.Name] = currentNode;
                    priorityQueue.Add((neighbor.Name, newDistance));
                }
            }
        }

        var path = new List<string>();
        var nodeN = toNodeName;

        while (previousNodes.ContainsKey(nodeN))
        {
            path.Insert(0, nodeN);
            nodeN = previousNodes[nodeN].Name;
        }

        if (path.Count > 0) path.Insert(0, fromNodeName);

        return new ShortestPathDataDTO
        {
            NodeNames = path,
            Distance = distances[toNodeName]
        };
    }

    public ShortestPathDataDTO FindShortestPathV2(string fromNode, string toNode, List<Node> graph)
    {
        var distances = new Dictionary<string, int>();
        var previousNodes = new Dictionary<string, string>();
        var priorityQueue = new SortedSet<(int Distance, string Node)>();
        var visitedNodes = new HashSet<string>();

        // Initialize distances and previous nodes
        foreach (var node in graph)
        {
            distances[node.Name] = int.MaxValue;
            previousNodes[node.Name] = string.Empty;
        }

        distances[fromNode] = 0;
        priorityQueue.Add((0, fromNode));

        while (priorityQueue.Count > 0)
        {
            // Get the node with the smallest distance
            var (currentDistance, currentNode) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (visitedNodes.Contains(currentNode))
                continue;

            visitedNodes.Add(currentNode);

            // Get the neighbors of the current node
            var node = graph.First(n => n.Name == currentNode);
            foreach (var edge in node.Edges)
            {
                var neighbor = edge.TargetNode;
                var newDistance = currentDistance + edge.Weight;

                if (newDistance < distances[neighbor.Name])
                {
                    // Update the shortest distance to the neighbor
                    distances[neighbor.Name] = newDistance;
                    previousNodes[neighbor.Name] = currentNode;

                    priorityQueue.Add((newDistance, neighbor.Name));
                }
            }
        }

        // Backtrack to construct the path
        var path = new List<string>();
        var current = toNode;

        while (current != null)
        {
            path.Insert(0, current);
            current = previousNodes[current];
        }

        if (distances[toNode] == int.MaxValue)
        {
            // No path exists
            return new ShortestPathDataDTO
            {
                NodeNames = [],
                Distance = int.MaxValue
            };
        }

        return new ShortestPathDataDTO
        {
            NodeNames = path,
            Distance = distances[toNode]
        };
    }

    public ShortestPathDataDTO FindShortestPathV3(string fromNode, string toNode, List<Node> graph)
    {
        if (string.IsNullOrWhiteSpace(fromNode) || string.IsNullOrWhiteSpace(toNode))
        {
            throw new ArgumentException("Both fromNode and toNode must be provided.");
        }

        if (!graph.Any(n => n.Name == fromNode))
        {
            throw new KeyNotFoundException($"The node '{fromNode}' does not exist in the graph.");
        }

        if (!graph.Any(n => n.Name == toNode))
        {
            throw new KeyNotFoundException($"The node '{toNode}' does not exist in the graph.");
        }

        var distances = new Dictionary<string, int>();
        var previousNodes = new Dictionary<string, string>();
        var priorityQueue = new SortedSet<(int Distance, string Node)>();
        var visitedNodes = new HashSet<string>();

        foreach (var node in graph)
        {
            distances[node.Name] = int.MaxValue;
            previousNodes[node.Name] = string.Empty; // Initialize to empty string for all nodes
        }

        distances[fromNode] = 0;
        priorityQueue.Add((0, fromNode));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentNode) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (visitedNodes.Contains(currentNode))
                continue;

            visitedNodes.Add(currentNode);

            var node = graph.First(n => n.Name == currentNode);
            foreach (var edge in node.Edges)
            {
                var neighbor = edge.TargetNode.Name;
                var newDistance = currentDistance + edge.Weight;

                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    previousNodes[neighbor] = currentNode;
                    priorityQueue.Add((newDistance, neighbor));
                }
            }
        }

        if (distances[toNode] == int.MaxValue)
        {
            // No path exists
            throw new Exception($"No path exists from {fromNode} to {toNode}.");
            // return new ShortestPathDataDTO
            // {
            //     NodeNames = [],
            //     Distance = int.MaxValue
            // };
        }

        var path = new List<string>();
        var current = toNode;
        while (current != null)
        {
            if (!previousNodes.ContainsKey(current))
            {
                throw new KeyNotFoundException($"The node '{current}' is missing from the previous nodes.");
            }
            path.Insert(0, current);
            current = previousNodes[current];
        }

        return new ShortestPathDataDTO
        {
            NodeNames = path,
            Distance = distances[toNode]
        };
    }




}
