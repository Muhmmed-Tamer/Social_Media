using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Social_Media.Services.AbstractsServicesOFSpecialModels;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> Logger;
        private readonly IWebHostEnvironment Hosting;
        public FileService(ILogger<FileService> Logger, IWebHostEnvironment Hosting)
        {
            this.Logger = Logger;
            this.Hosting = Hosting;
        }

        public Task<bool> DeleteFile(string FileNameAndExtension, string DirectoryThatStoreFileIn)
        {
            try
            {
                string FullPathOfDirectory = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn);
                string FullPathOfFile = Path.Combine(FullPathOfDirectory, FileNameAndExtension);
                if (File.Exists(FullPathOfFile))
                {
                    File.Delete(FullPathOfFile);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<(string, bool)> GeneratePathOFFile(IFormFile File, long MaxSize, string DirectoryThatStoreFileIn, string[] AllowedExtension)
        {
            try
            {
                string ExtensionOFFile = Path.GetExtension(File.FileName);
                if (File.Length <= MaxSize & File.Length > 0 & AllowedExtension.Contains(ExtensionOFFile))
                {
                    string NewFileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    string CurrentFileName = File.Name;
                    CurrentFileName = string.Concat(NewFileName, ExtensionOFFile);
                    string FullPathOfDirectory = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn);
                    string FullPathOfFileOFImage = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn, CurrentFileName);
                    if (!Directory.Exists(FullPathOfDirectory))
                    {
                        Directory.CreateDirectory(FullPathOfDirectory);
                    }
                    await File.CopyToAsync(new FileStream(FullPathOfFileOFImage, FileMode.Create));
                    return (CurrentFileName, true);
                }
                return (string.Empty, false);
            }
            catch (Exception ex)
            {
                Logger.LogWarning("");
                return (ex.Message, false);
            }
        }
    }
}
