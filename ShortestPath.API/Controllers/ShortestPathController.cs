using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ShortestPath.API.Model;
using ShortestPath.Core.DTO;
using ShortestPath.Core.Repositories;

namespace ShortestPath.API.Controllers;

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

        //Console.WriteLine(JsonSerializer.Serialize(request));

        var result = _shortPathRepository.FindShortestPath(request.FromNode, request.ToNode, request.GraphNodes);
        return Ok(result);
    }
}