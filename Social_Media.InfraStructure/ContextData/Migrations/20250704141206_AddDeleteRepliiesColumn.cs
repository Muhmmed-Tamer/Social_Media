using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteRepliiesColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReplyOFComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ReplyOFComments");
        }
    }
}
