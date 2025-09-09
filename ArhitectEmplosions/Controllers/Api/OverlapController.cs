using ArhitectEmplosions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



[ApiController]
[Route("api/[controller]")]
public class OverlapController : ControllerBase
{
    private readonly OverlapService _overlapService;
    public OverlapController(OverlapService overlapService)
    {
        _overlapService = overlapService;
    }

    [HttpGet("geometry")]
    public async Task<IActionResult> GetOverlapGeometry()
    {
        await _overlapService.GetOverlapGeometryAsync();
        return Ok(new { message = "Overlap geometry processing initiated." });
    }

}

