using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHubNameFromUserConnectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HubName",
                table: "UserConnections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HubName",
                table: "UserConnections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
