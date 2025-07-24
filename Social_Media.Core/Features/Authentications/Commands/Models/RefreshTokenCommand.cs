using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Helpers.Models;

namespace Social_Media.Core.Features.Authentications.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JWTTokenResult>>
    {
        public string UserAccessToken { get; set; }
        public string UserRefreshToken { get; set; }
        public RefreshTokenCommand()
        {

        }

        public RefreshTokenCommand(string UserAccessToken, string UserRefreshToken)
        {
            this.UserAccessToken = UserAccessToken;
            this.UserRefreshToken = UserRefreshToken;
        }
    }
}
