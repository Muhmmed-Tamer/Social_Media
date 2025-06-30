using Microsoft.AspNetCore.Http;
using Social_Media.Data.Enums;

namespace Social_Media.Services.AbstractsServicesOFSpecialModels
{
    public interface IFileService
    {
        Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn, long OtherMaxSize, string OtherDirectoryThatStoreFileIn, string[] OtherAllowedExtension);
        Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn);
        Task<(string, bool)> GeneratePathOFFile(IFormFile File, long MaxSize, string DirectoryThatStoreFileIn, string[] AllowedExtension);
        Task<bool> DeleteFile(string FileNameAndExtension, string DirectoryThatStoreFileIn);
        Task<bool> DeleteFiles(List<string> FileNameAndExtension, string DirectoryThatStoreImagesInIt, string DirectoryThatStoreVideosInIt);
        Task<ExtensionType> GetExtensionTypeOFFile(string ExtensionOFFile, string[] AllowedExtensionsOFImage = null, string[] AllowedExtensionsOFVideos = null, string[] AllowedExtensionsOFAudios = null);
    }
}
