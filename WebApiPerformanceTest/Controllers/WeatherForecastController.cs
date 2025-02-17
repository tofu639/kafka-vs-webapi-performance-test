using Microsoft.AspNetCore.Mvc;

namespace WebApiPerformanceTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            // Simulate some processing
            return Ok(new { Message = "Received: " + message });
        }
    }
}