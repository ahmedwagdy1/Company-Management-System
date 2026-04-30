using Microsoft.AspNetCore.Http;

namespace Application.BLL.Services.Attachment_Service
{
    public interface IAttachmentService
    {
        // Uplode
        public Task<string?> UploadAsync(IFormFile file, string folderName);
        // Delete
        public bool Delete(string filePath);
    }
}
