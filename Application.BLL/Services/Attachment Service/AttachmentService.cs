using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.BLL.Services.Attachment_Service
{
    public class AttachmentService : IAttachmentService
    {
        readonly List<string> allowedExtensions = [".jpg", ".jpeg", ".png"];
        const int MaxSize = 2_097_152; 
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            // 1. Check Extension
            var extension = Path.GetExtension(file.FileName); // Ahmed.png => .png
            if (!allowedExtensions.Contains(extension)) return null;

            // 2. Check Size (2mg)
            if(file.Length > MaxSize || file.Length == 0) return null;

            // 3. Get Local Path
            //var folderPath = "C:\\Users\\kh\\OneDrive\\Desktop\\Route Course\\back end .net\\Revesion (mariam shindy)\\MVC\\MVC Project\\Application.Presentation.Solution\\Application.Presentation\\wwwroot\\files\\images\\"
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            // 4. Make Attachment Name Unique
            var fileName = $"{Guid.NewGuid()}_{file.FileName}"; // 1234567890_cat.png

            // 5. Get File Path
            var filePath = Path.Combine(folderPath, fileName); // Application.Presentation\\wwwroot\\files\\images\\1234567890_cat.png

            // 6. Create File Stream
            using FileStream stream = new FileStream(filePath, FileMode.Create);

            // 7. Use Stream to Copy The File
            await file.CopyToAsync(stream);

            // 8. Return File Name To Store in db
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
