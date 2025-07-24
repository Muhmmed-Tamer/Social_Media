using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class PostUnitOFWork : IPostUnitOFWork
    {
        public PostUnitOFWork(IImageOrVideoPathServices ImageOrVideoPathServices, IPostServices PostServices)
        {
            this.ImageOrVideoPathServices = ImageOrVideoPathServices;
            this.PostServices = PostServices;
        }

        public IImageOrVideoPathServices ImageOrVideoPathServices { get; }

        public IPostServices PostServices { get; }
    }
}
