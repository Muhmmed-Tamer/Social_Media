using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.FriendsServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class FriendUnitOFWork : IFriendUnitOFWork
    {
        public FriendUnitOFWork(IFriendRequestServices FriendRequestServices, IFriendServices FriendServices)
        {
            this.FriendRequestServices = FriendRequestServices;
            this.FriendServices = FriendServices;
        }

        public IFriendRequestServices FriendRequestServices { get; }

        public IFriendServices FriendServices { get; }
    }
}
