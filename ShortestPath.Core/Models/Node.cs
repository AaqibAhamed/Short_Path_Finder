namespace ShortestPath.Core.Entities;

public class Node
{
    public string Name { get; set; } = string.Empty;
    public List<Edge> Edges { get; set; } = [];
}