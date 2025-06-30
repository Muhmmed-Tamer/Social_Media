namespace Social_Media.RealTimeServices.AbstractsHubServices.PostHub
{
    public interface IPostHubServices
    {
        Task ReceivePost(object Data);
    }
}
