using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.LohikaBackend.Migrations
{
    public partial class Addproductbycategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "tblProducts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CategoryId",
                table: "tblProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblCategories_CategoryId",
                table: "tblProducts",
                column: "CategoryId",
                principalTable: "tblCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblCategories_CategoryId",
                table: "tblProducts");

            migrationBuilder.DropIndex(
                name: "IX_tblProducts_CategoryId",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tblProducts");
        }
    }
}
