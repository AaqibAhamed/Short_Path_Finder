using Microsoft.AspNetCore.Mvc;
using ShortestPath.Core.DTO;

[ApiController]
[Route("api/[controller]")]
public class ShortestPathController : ControllerBase
{
    private readonly IShortPathRepository _shortPathRepository;

    public ShortestPathController(IShortPathRepository shortPathRepository)
    {
        _shortPathRepository = shortPathRepository ?? throw new ArgumentNullException(nameof(shortPathRepository));
    }

    [HttpPost("shortest-path")]
    public ActionResult<ShortestPathDataDTO> CalculateShortestPath([FromBody] ShortestPathRequest request)
    {
        if (request.FromNode == null || request.ToNode == null || request.GraphNodes == null)
        {
            return BadRequest("Invalid request data.");
        }

        var result = _shortPathRepository.FindShortestPath(request.FromNode, request.ToNode, request.GraphNodes);
        return Ok(result);
    }
}

