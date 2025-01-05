using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core.Repositories;

public interface IShortPathRepository
{
    ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph);

    ShortestPathDataDTO FindShortestPathV2(string fromNodeName, string toNodeName, List<Node> graph);

    ShortestPathDataDTO FindShortestPathV3(string fromNodeName, string toNodeName, List<Node> graph);
}