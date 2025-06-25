using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Social_Media.Data.Enums;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels
{
    public class FileService : IFileService
    {
        private readonly IFileConfigurationServices<ConfigurationOFPostImageServices> PostImageConfigurationServices;
        private readonly IFileConfigurationServices<ConfigurationOFPostVideoServices> PostVideoConfigurationServices;
        private readonly IWebHostEnvironment Hosting;
        private readonly Serilog.ILogger Logger;
        public FileService(Serilog.ILogger Logger, IWebHostEnvironment Hosting, IFileConfigurationServices<ConfigurationOFPostImageServices> PostImageConfigurationServices, IFileConfigurationServices<ConfigurationOFPostVideoServices> PostVideoConfigurationServices)
        {
            this.PostImageConfigurationServices = PostImageConfigurationServices;
            this.PostVideoConfigurationServices = PostVideoConfigurationServices;
            this.Hosting = Hosting;
            this.Logger = Logger;
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
                Logger.Error(ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteFiles(List<string> FileNameAndExtension)
        {
            try
            {

                if (FileNameAndExtension is null || FileNameAndExtension.Count == 0)
                {
                    return false;
                }
                List<string> FullPathOfFiles = new();
                foreach (string FileName in FileNameAndExtension)
                {
                    string DirectoryThatStoreFileIn = string.Empty;
                    ExtensionType ExtensionType = await GetExtensionTypeOFFile(Path.GetExtension(FileName));
                    if (ExtensionType == ExtensionType.Image)
                    {
                        DirectoryThatStoreFileIn = PostImageConfigurationServices.DirectoryThatStoreFileIn();
                    }
                    else if (ExtensionType == ExtensionType.Video)
                    {
                        DirectoryThatStoreFileIn = PostVideoConfigurationServices.DirectoryThatStoreFileIn();
                    }
                    else if (ExtensionType == ExtensionType.None)
                    {
                        Logger.Error($"File with name {FileName} has an unsupported extension.");
                        return false;
                    }
                    string FullPathOFFile = Path.Combine(Hosting.WebRootPath, DirectoryThatStoreFileIn, FileName);
                    FullPathOfFiles.Add(FullPathOFFile);
                }
                foreach (string FullPathOFFile in FullPathOfFiles)
                {
                    if (File.Exists(FullPathOFFile))
                    {
                        File.Delete(FullPathOFFile);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
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
                Logger.Error(ex.Message);
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
                Logger.Error(ex.Message);
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
                Logger.Error(ex.Message);
                return (new List<string>() { ex.Message }, false);
            }
        }

        public Task<ExtensionType> GetExtensionTypeOFFile(string ExtensionOFFile)
        {
            try
            {
                if (PostImageConfigurationServices.AllowedExtension().Contains(ExtensionOFFile))
                {
                    return Task.FromResult(ExtensionType.Image);
                }
                else if (PostVideoConfigurationServices.AllowedExtension().Contains(ExtensionOFFile))
                {
                    return Task.FromResult(ExtensionType.Video);
                }
                return Task.FromResult(ExtensionType.None);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
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
                Logger.Error(ex.Message);
                return ex.Message;
            }
        }
    }
}
