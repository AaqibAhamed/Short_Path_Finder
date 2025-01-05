using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core.Repositories;

public interface IShortPathRepository
{
    ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph);
}