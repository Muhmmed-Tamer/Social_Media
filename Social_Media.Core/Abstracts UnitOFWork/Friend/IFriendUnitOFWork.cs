using Social_Media.Services.AbstractsServices.FriendsServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IFriendUnitOFWork
    {
        public IFriendRequestServices FriendRequestServices { get; }
        public IFriendServices FriendServices { get; }
    }
}
