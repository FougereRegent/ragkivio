using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ragkivio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                    TRUNCATE TABLE users
                    """);

            migrationBuilder.DropIndex(
                name: "ix_users_email_auth_id",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "ix_users_auth_id",
                table: "users",
                column: "auth_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_auth_id",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "ix_users_email_auth_id",
                table: "users",
                columns: new[] { "email", "auth_id" },
                unique: true);
        }
    }
}