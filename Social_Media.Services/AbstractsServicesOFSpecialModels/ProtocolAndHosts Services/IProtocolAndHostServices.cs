namespace Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services
{
    public interface IProtocolAndHostServices
    {
        string GetProtocolAndHost();
        string GetFullPathOFFile(string BaseDirectory, string? SubDirectory = null, string? FileName = null);
        string GoToEndPointInMyApplication(string Controller, string Action, object? Data);
    }
}
