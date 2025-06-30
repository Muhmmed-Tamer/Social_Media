using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Models
{
    public class AddInteractionWithPostCommand : IRequest<Response<string>>
    {
        public AddInteractionWithPostCommand()
        {

        }
        public string UserIdThatInteractWithPost { get; set; }
        public InteractionType InteractionBy { get; set; }
        public int PostIdThatInteractWith { get; set; }
        public AddInteractionWithPostCommand(string UserIdThatInteractWithPost, InteractionType InteractionBy, int PostIdThatInteractWith)
        {
            this.UserIdThatInteractWithPost = UserIdThatInteractWithPost;
            this.InteractionBy = InteractionBy;
            this.PostIdThatInteractWith = PostIdThatInteractWith;
        }
    }
}
