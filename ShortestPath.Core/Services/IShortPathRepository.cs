
using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

public interface IShortPathRepository
{
    ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph);
}

