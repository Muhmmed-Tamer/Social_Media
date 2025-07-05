using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Identity;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Services.ImplementationServices.IdentityServices.IdentityUser
{
    public class UserServices : IUserServices
    {
        private readonly Serilog.ILogger Logger;
        public UserManager<ApplicationUser> ManagerUser { get; }

        public UserServices(UserManager<ApplicationUser> MangerUser, Serilog.ILogger Logger)
        {
            this.ManagerUser = MangerUser;
            this.Logger = Logger;
        }

        public async Task<ApplicationUser> GetUserByPhoneNumberAsync(string PhoneNumber)
        {
            try
            {
                return await ManagerUser.Users.Where(U => U.PhoneNumber == PhoneNumber).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<string> GetUserIdThatOwnedPost(int PostId)
        {
            try
            {
                return await ManagerUser.Users.Where(P => P.Posts.Any(P => P.Id == PostId && !P.IsDeleted)).Select(U => U.Id).FirstAsync();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<string> GetUserIdThatOwnedComment(int CommentId)
        {
            try
            {
                return await ManagerUser.Users
                    .Where(C => C.Comments.Any(C => C.Id == CommentId))
                    .Select(U => U.Id)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
