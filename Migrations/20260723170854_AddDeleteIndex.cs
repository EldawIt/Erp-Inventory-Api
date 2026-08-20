using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_IsDeleted_Id",
                table: "ProductItems",
                columns: new[] { "IsDeleted", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductItems_IsDeleted_Id",
                table: "ProductItems");
        }
    }
}
