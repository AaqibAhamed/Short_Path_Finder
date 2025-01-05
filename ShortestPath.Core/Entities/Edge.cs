namespace ShortestPath.Core.Entities;

public class Edge
{
    public Node TargetNode { get; set; }
    public int Weight { get; set; }
}