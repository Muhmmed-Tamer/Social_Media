namespace Social_Media.RealTimeServices.AbstractsHubServices.CommentHub
{
    public interface ICommentHubServices
    {
        Task ReceiveComment(object Data);
    }
}
