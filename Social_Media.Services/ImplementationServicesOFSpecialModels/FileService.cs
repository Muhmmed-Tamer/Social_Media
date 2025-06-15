using ConstantStatementInAllProject.Files;
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
                Logger.LogError(ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<(string, bool)> GeneratePathOFFile(IFormFile File, long MaxSize, string DirectoryThatStoreFileIn, string[] AllowedExtension)
        {
            try
            {
                string ExtensionOFFile = Path.GetExtension(File.FileName);
                if (AllowedExtension.Contains(ExtensionOFFile))
                {
                    if (File.Length <= MaxSize & File.Length > 0)
                    {
                        string CurrentFileName = await GenerateNewFileNameAndExtension(File, ExtensionOFFile, DirectoryThatStoreFileIn);
                        return (CurrentFileName, true);
                    }
                    return (FilesConstants.ErrorSizeFile, false);
                }
                return (FilesConstants.ErrorExtensionFile, false);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return (ex.Message, false);
            }
        }

        public async Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn)
        {
            try
            {
                List<string> FilePaths = new();
                foreach (var SingleFile in Files)
                {
                    string ExtensionOFFile = Path.GetExtension(SingleFile.FileName);
                    if (AllowedExtension.Contains(ExtensionOFFile))
                    {
                        if (SingleFile.Length <= MaxSize && SingleFile.Length > 0)
                        {
                            string CurrentFileName = await GenerateNewFileNameAndExtension(SingleFile, ExtensionOFFile, DirectoryThatStoreFileIn);
                            FilePaths.Add(CurrentFileName);
                        }
                        else
                        {
                            FilePaths.Add(FilesConstants.ErrorSizeFiles);
                        }
                    }
                    FilePaths.Add(FilesConstants.ErrorExtensionFiles);
                    return (FilePaths, false);
                }
                return (FilePaths, true);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return (new List<string>() { ex.Message }, false);
            }
        }

        public async Task<(List<string>, bool)> GeneratePathOFFiles(List<IFormFile> Files, long MaxSize, string[] AllowedExtension, string DirectoryThatStoreFileIn, long OtherMaxSize, string OtherDirectoryThatStoreFileIn, string[] OtherAllowedExtension)
        {
            try
            {
                List<string> FilePaths = new();
                foreach (var SingleFile in Files)
                {
                    string ExtensionOFFile = Path.GetExtension(SingleFile.FileName);
                    if (AllowedExtension.Contains(ExtensionOFFile))
                    {
                        if (SingleFile.Length <= MaxSize && SingleFile.Length > 0)
                        {
                            string CurrentFileName = await GenerateNewFileNameAndExtension(SingleFile, ExtensionOFFile, DirectoryThatStoreFileIn);
                            FilePaths.Add(CurrentFileName);
                        }
                        else
                        {
                            FilePaths.Add(FilesConstants.ErrorSizeFiles);
                            return (FilePaths, false);
                        }
                    }
                    else if (OtherAllowedExtension.Contains(ExtensionOFFile))
                    {
                        if (SingleFile.Length <= OtherMaxSize && SingleFile.Length > 0)
                        {
                            string CurrentFileName = await GenerateNewFileNameAndExtension(SingleFile, ExtensionOFFile, OtherDirectoryThatStoreFileIn);
                            FilePaths.Add(CurrentFileName);
                        }
                        else
                        {
                            FilePaths.Add(FilesConstants.ErrorSizeFiles);
                            return (FilePaths, false);
                        }
                    }
                    else
                    {
                        FilePaths.Add(FilesConstants.ErrorExtensionFiles);
                        return (FilePaths, false);
                    }
                }
                return (FilePaths, true);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return (new List<string>() { ex.Message }, false);
            }
        }
        private async Task<string> GenerateNewFileNameAndExtension(IFormFile FileData, string ExtensionOFFile, string DirectoryThatStoreFileIn)
        {
            try
            {
                string NewFileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                string CurrentFileName = FileData.Name;
                CurrentFileName = string.Concat(NewFileName, ExtensionOFFile);
                string FullPathOfDirectory = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn);
                string FullPathOfFileOFImage = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn, CurrentFileName);
                if (!Directory.Exists(FullPathOfDirectory))
                {
                    Directory.CreateDirectory(FullPathOfDirectory);
                }
                using (var stream = new FileStream(FullPathOfFileOFImage, FileMode.Create))
                {
                    await FileData.CopyToAsync(stream);
                }
                return CurrentFileName;
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return ex.Message;
            }
        }
    }
}
