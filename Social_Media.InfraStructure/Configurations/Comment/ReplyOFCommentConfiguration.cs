using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Comments;

namespace Social_Media.InfraStructure.Configurations.Comment
{
    public class ReplyOFCommentConfiguration : IEntityTypeConfiguration<ReplyOFComment>
    {
        public void Configure(EntityTypeBuilder<ReplyOFComment> builder)
        {
            builder.HasOne(C => C.Comment).WithMany(RC => RC.ReplyOFComments).HasForeignKey(RC => RC.CommentId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(C => C.UserThatWriteAComment).WithMany(RC => RC.WriteAComments).HasForeignKey(RC => RC.UserIdThatWriteAComment).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(C => C.UserThatWriteAReplyOFComment).WithMany(RC => RC.WriteAReplyOFComments).HasForeignKey(RC => RC.UserIdThatWriteAReplyOFComment).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
