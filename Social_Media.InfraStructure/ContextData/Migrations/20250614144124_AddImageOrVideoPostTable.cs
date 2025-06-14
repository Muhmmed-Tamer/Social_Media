using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class AddImageOrVideoPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ImageOrVideoPost_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageOrVideoPost_AspNetUsers_UserId",
                table: "ImageOrVideoPost");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionNotificationByPosts_ImageOrVideoPost_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionNotificationByPosts_Posts_TextPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionWithPosts_ImageOrVideoPost_ImageOrVideoPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionWithPosts_Posts_TextPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostNotifications_ImageOrVideoPost_ImageOrVideoPostId",
                table: "PostNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PostNotifications_Posts_TextPostId",
                table: "PostNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageOrVideoPost",
                table: "ImageOrVideoPost");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "TextPosts");

            migrationBuilder.RenameTable(
                name: "ImageOrVideoPost",
                newName: "ImageOrVideoPosts");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "TextPosts",
                newName: "IX_TextPosts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageOrVideoPost_UserId",
                table: "ImageOrVideoPosts",
                newName: "IX_ImageOrVideoPosts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextPosts",
                table: "TextPosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageOrVideoPosts",
                table: "ImageOrVideoPosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ImageOrVideoPosts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "ImageOrVideoPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TextPosts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "TextPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageOrVideoPosts_AspNetUsers_UserId",
                table: "ImageOrVideoPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionNotificationByPosts_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionNotificationByPosts_TextPosts_TextPostId",
                table: "InteractionNotificationByPosts",
                column: "TextPostId",
                principalTable: "TextPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionWithPosts_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "InteractionWithPosts",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionWithPosts_TextPosts_TextPostId",
                table: "InteractionWithPosts",
                column: "TextPostId",
                principalTable: "TextPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostNotifications_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "PostNotifications",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostNotifications_TextPosts_TextPostId",
                table: "PostNotifications",
                column: "TextPostId",
                principalTable: "TextPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextPosts_AspNetUsers_UserId",
                table: "TextPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ImageOrVideoPosts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TextPosts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageOrVideoPosts_AspNetUsers_UserId",
                table: "ImageOrVideoPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionNotificationByPosts_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionNotificationByPosts_TextPosts_TextPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionWithPosts_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionWithPosts_TextPosts_TextPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostNotifications_ImageOrVideoPosts_ImageOrVideoPostId",
                table: "PostNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PostNotifications_TextPosts_TextPostId",
                table: "PostNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TextPosts_AspNetUsers_UserId",
                table: "TextPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextPosts",
                table: "TextPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageOrVideoPosts",
                table: "ImageOrVideoPosts");

            migrationBuilder.RenameTable(
                name: "TextPosts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "ImageOrVideoPosts",
                newName: "ImageOrVideoPost");

            migrationBuilder.RenameIndex(
                name: "IX_TextPosts_UserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageOrVideoPosts_UserId",
                table: "ImageOrVideoPost",
                newName: "IX_ImageOrVideoPost_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageOrVideoPost",
                table: "ImageOrVideoPost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ImageOrVideoPost_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "ImageOrVideoPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageOrVideoPost_AspNetUsers_UserId",
                table: "ImageOrVideoPost",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionNotificationByPosts_ImageOrVideoPost_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPost",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionNotificationByPosts_Posts_TextPostId",
                table: "InteractionNotificationByPosts",
                column: "TextPostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionWithPosts_ImageOrVideoPost_ImageOrVideoPostId",
                table: "InteractionWithPosts",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPost",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionWithPosts_Posts_TextPostId",
                table: "InteractionWithPosts",
                column: "TextPostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostNotifications_ImageOrVideoPost_ImageOrVideoPostId",
                table: "PostNotifications",
                column: "ImageOrVideoPostId",
                principalTable: "ImageOrVideoPost",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostNotifications_Posts_TextPostId",
                table: "PostNotifications",
                column: "TextPostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
