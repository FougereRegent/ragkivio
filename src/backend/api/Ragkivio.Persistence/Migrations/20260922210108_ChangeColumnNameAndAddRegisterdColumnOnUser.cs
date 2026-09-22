using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ragkivio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameAndAddRegisterdColumnOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isdelete",
                table: "users",
                newName: "is_delete");

            migrationBuilder.RenameColumn(
                name: "deletedat",
                table: "users",
                newName: "deleted_at");

            migrationBuilder.AddColumn<bool>(
                name: "is_registered",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_registered",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "is_delete",
                table: "users",
                newName: "isdelete");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "users",
                newName: "deletedat");
        }
    }
}
