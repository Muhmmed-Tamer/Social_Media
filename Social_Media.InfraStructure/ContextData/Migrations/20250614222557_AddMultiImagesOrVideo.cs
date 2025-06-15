using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiImagesOrVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOrVideoPath",
                table: "ImageOrVideoPosts");

            migrationBuilder.CreateTable(
                name: "ImageOrVideoPath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Image_Or_VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOrVideoPath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageOrVideoPath_ImageOrVideoPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "ImageOrVideoPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageOrVideoPath_PostId",
                table: "ImageOrVideoPath",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageOrVideoPath");

            migrationBuilder.AddColumn<string>(
                name: "ImageOrVideoPath",
                table: "ImageOrVideoPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
