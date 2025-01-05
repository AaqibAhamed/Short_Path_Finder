using ShortestPath.Core.DTO;
using ShortestPath.Core.Entities;

namespace ShortestPath.Core.Services;

public class ShortPathService : IShortPathService
{
    // since API input/output same as the domain models - mapping not required 
    public ShortestPathDataDTO FindShortestPath(string fromNodeName, string toNodeName, List<Node> graph)
    {
        // since API input/output same as the domain models - mapping not required 
        throw new NotImplementedException();
    }
}