using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ragkivio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMimeDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_documents_checksum_tenantid",
                table: "documents",
                columns: new[] { "checksum", "tenant_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_documents_checksum_tenantid",
                table: "documents");
        }
    }
}
