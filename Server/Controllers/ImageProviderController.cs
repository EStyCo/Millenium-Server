using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageProviderController : ControllerBase
    {
        [HttpGet("{category}/{fileName}")]
        public IActionResult GetImage(string category, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ImageProvider", category, fileName);

            if (!System.IO.File.Exists(path))
                return NotFound();

            var image = System.IO.File.OpenRead(path);
            return File(image, "image/png");
        }
    }
}
