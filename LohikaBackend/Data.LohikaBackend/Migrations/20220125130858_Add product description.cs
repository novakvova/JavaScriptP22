using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.LohikaBackend.Migrations
{
    public partial class Addproductdescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblProducts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblProducts");
        }
    }
}
