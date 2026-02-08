using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace UploadImageToAFolder.Controllers
{
    [ApiController]
    [Route("api/ImageUpload")]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return BadRequest("No File Uploaded.");
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents", "ProjectPics");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(directory, fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return Ok(new { filePath });
        }
    }   
}