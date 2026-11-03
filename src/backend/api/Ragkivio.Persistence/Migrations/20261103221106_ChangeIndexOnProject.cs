using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ragkivio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIndexOnProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_projects_name_tenantid",
                table: "projects");

            migrationBuilder.CreateIndex(
                name: "ix_projects_name_tenantid_isdelete_createdat",
                table: "projects",
                columns: new[] { "name", "tenant_id", "is_delete", "created_at" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_projects_name_tenantid_isdelete_createdat",
                table: "projects");

            migrationBuilder.CreateIndex(
                name: "ix_projects_name_tenantid",
                table: "projects",
                columns: new[] { "name", "tenant_id" },
                unique: true);
        }
    }
}
