using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core.Services;

public interface IShortPathService
{
    ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph);
}