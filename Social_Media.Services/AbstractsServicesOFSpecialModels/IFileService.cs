using Microsoft.AspNetCore.Http;

namespace Social_Media.Services.AbstractsServicesOFSpecialModels
{
    public interface IFileService
    {
        Task<(string, bool)> GeneratePathOFFile(IFormFile File, long MaxSize, string DirectoryThatStoreFileIn, string[] AllowedExtension);
        Task<bool> DeleteFile(string FileNameAndExtension, string DirectoryThatStoreFileIn);
    }
}
