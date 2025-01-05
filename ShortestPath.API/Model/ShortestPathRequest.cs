using ShortestPath.Core.Entities;

public class ShortestPathRequest
{
    public required string FromNode { get; set; }
    public string? ToNode { get; set; }
    public List<Node>? GraphNodes { get; set; }
}