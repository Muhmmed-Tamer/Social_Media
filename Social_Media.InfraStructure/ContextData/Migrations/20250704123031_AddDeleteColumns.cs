using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Story",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SendFriendRequestNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "SendFriendRequestNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PostNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "PostNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MessageNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "MessageNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InteractionWithPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InteractionWithComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InteractionNotificationByStories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "InteractionNotificationByStories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InteractionNotificationByPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "InteractionNotificationByPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InteractionNotificationByComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "InteractionNotificationByComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ImageOrVideoPaths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ImageOrVideoPaths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "FriendRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FriendRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ConfirmFriendRequestNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ConfirmFriendRequestNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CommentNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "CommentNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SendFriendRequestNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "SendFriendRequestNotifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PostNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "PostNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MessageNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "MessageNotifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InteractionWithPosts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InteractionWithComments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InteractionNotificationByStories");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "InteractionNotificationByStories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InteractionNotificationByComments");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "InteractionNotificationByComments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ImageOrVideoPaths");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ImageOrVideoPaths");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConfirmFriendRequestNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ConfirmFriendRequestNotifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CommentNotifications");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "CommentNotifications");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");
        }
    }
}
