using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace UploadImageToAFolder.Controllers
{
    [ApiController]
    [Route("api/ImageUpload")]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [HttpGet("{FileName}", Name = "GetImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetImage(string FileName)
        {
            if (string.IsNullOrEmpty(FileName)) return BadRequest("Invalid File Name.");
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents", "ProjectPics");
            var filePath = Path.Combine(directory, FileName);
            if (!System.IO.File.Exists(filePath)) return NotFound("Image Not Found.");
            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);
            return File(image, mimeType);
        }


        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }   
}