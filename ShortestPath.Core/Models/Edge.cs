namespace ShortestPath.Core.Entities;

public class Edge
{
    public required Node TargetNode { get; set; }
    public int Weight { get; set; }
}