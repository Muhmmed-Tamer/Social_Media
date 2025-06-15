using Microsoft.AspNetCore.Http;

namespace Social_Media.Services.AbstractsServicesOFSpecialModels
{
    public interface IFileService
    {
        Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn, long OtherMaxSize, string OtherDirectoryThatStoreFileIn, string[] OtherAllowedExtension);
        Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn);
        Task<(string, bool)> GeneratePathOFFile(IFormFile File, long MaxSize, string DirectoryThatStoreFileIn, string[] AllowedExtension);
        Task<bool> DeleteFile(string FileNameAndExtension, string DirectoryThatStoreFileIn);
    }
}
