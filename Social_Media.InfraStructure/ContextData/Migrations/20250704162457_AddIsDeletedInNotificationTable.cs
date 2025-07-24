using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.InfraStructure.ContextData.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedInNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "Notification",
                newName: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Notification",
                newName: "IsUpdated");
        }
    }
}
