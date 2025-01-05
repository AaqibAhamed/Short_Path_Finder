using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core;

public class DijkstraAlgorithm
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
}
