using Microsoft.AspNetCore.Identity;
using Social_Media.Data.Identity;

namespace Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser
{
    public interface IUserServices
    {
        public UserManager<ApplicationUser> ManagerUser { get; }
        Task<ApplicationUser> GetUserByPhoneNumberAsync(string PhoneNumber);
        Task<string> GetUserIdThatOwnedPost(int PostId);
        Task<string> GetUserIdThatOwnedComment(int CommentId);
    }
}
