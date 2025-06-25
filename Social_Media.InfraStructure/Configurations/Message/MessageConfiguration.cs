using Microsoft.EntityFrameworkCore;

namespace Social_Media.InfraStructure.Configurations.Message
{
    public class MessageConfiguration : IEntityTypeConfiguration<Data.Models.Chat.Message>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Data.Models.Chat.Message> builder)
        {
            builder.HasOne(M => M.SenderUser).WithMany(U => U.SentMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(M => M.ReceiverUser).WithMany(U => U.ReceiveMessages).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
