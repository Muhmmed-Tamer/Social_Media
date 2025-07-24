using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IPostUnitOFWork
    {
        public IImageOrVideoPathServices ImageOrVideoPathServices { get; }
        public IPostServices PostServices { get; }
    }
}
