using Social_Media.Services.AbstractsServices.ChatServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IChatUnitOFWork
    {
        public IMessageMediaPathServices MessageMediaPathService { get; }
        public IMessageServices MessageServices { get; }
    }
}
