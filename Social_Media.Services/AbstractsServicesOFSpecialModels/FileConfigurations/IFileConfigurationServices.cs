namespace Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations
{
    public interface IFileConfigurationServices<T> where T : class
    {
        string DirectoryThatStoreFileIn();
        long MaxSize();
        string[] AllowedExtension();

    }
}
