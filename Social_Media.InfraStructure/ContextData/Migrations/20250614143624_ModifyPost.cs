using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteractionNotificationByPosts_Posts_PostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionWithPosts_Posts_PostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostNotifications_Posts_PostId",
                table: "PostNotifications");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications");

            migrationBuilder.DropIndex(
                name: "IX_InteractionWithPosts_PostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionNotificationByPosts_PostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Posts");



            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageOrVideoPostId",
                table: "PostNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextPostId",
                table: "PostNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageOrVideoPostId",
                table: "InteractionWithPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextPostId",
                table: "InteractionWithPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageOrVideoPostId",
                table: "InteractionNotificationByPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextPostId",
                table: "InteractionNotificationByPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageOrVideoPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageOrVideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Privacy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOrVideoPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageOrVideoPost_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_ImageOrVideoPostId",
                table: "PostNotifications",
                column: "ImageOrVideoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_TextPostId",
                table: "PostNotifications",
                column: "TextPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionWithPosts_ImageOrVideoPostId",
                table: "InteractionWithPosts",
                column: "ImageOrVideoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionWithPosts_TextPostId",
                table: "InteractionWithPosts",
                column: "TextPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionNotificationByPosts_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts",
                column: "ImageOrVideoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionNotificationByPosts_TextPostId",
                table: "InteractionNotificationByPosts",
                column: "TextPostId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageOrVideoPost_UserId",
                table: "ImageOrVideoPost",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ImageOrVideoPost_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "ImageOrVideoPost",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ImageOrVideoPost_PostId",
                table: "Comments");

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

            migrationBuilder.DropTable(
                name: "ImageOrVideoPost");

            migrationBuilder.DropIndex(
                name: "IX_PostNotifications_ImageOrVideoPostId",
                table: "PostNotifications");

            migrationBuilder.DropIndex(
                name: "IX_PostNotifications_TextPostId",
                table: "PostNotifications");

            migrationBuilder.DropIndex(
                name: "IX_InteractionWithPosts_ImageOrVideoPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionWithPosts_TextPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionNotificationByPosts_ImageOrVideoPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionNotificationByPosts_TextPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropColumn(
                name: "ImageOrVideoPostId",
                table: "PostNotifications");

            migrationBuilder.DropColumn(
                name: "TextPostId",
                table: "PostNotifications");

            migrationBuilder.DropColumn(
                name: "ImageOrVideoPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropColumn(
                name: "TextPostId",
                table: "InteractionWithPosts");

            migrationBuilder.DropColumn(
                name: "ImageOrVideoPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.DropColumn(
                name: "TextPostId",
                table: "InteractionNotificationByPosts");

            migrationBuilder.RenameColumn(
                name: "Name_LastNameInArabic",
                table: "AspNetUsers",
                newName: "Name_FirstNameInArabic");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionWithPosts_PostId",
                table: "InteractionWithPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionNotificationByPosts_PostId",
                table: "InteractionNotificationByPosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionNotificationByPosts_Posts_PostId",
                table: "InteractionNotificationByPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionWithPosts_Posts_PostId",
                table: "InteractionWithPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostNotifications_Posts_PostId",
                table: "PostNotifications",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
