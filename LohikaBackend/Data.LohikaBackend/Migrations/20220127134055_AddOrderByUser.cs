using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.LohikaBackend.Migrations
{
    public partial class AddOrderByUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblOrderStatusEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderStatusEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumerFirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ConsumerSecondName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ConsumerPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HomeNumber = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOrderEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOrderEntities_tblOrderStatusEntities_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tblOrderStatusEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderItemEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderItemEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOrderItemEntities_tblOrderEntities_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tblOrderEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOrderItemEntities_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderEntities_StatusId",
                table: "tblOrderEntities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderEntities_UserId",
                table: "tblOrderEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderItemEntities_OrderId",
                table: "tblOrderItemEntities",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderItemEntities_ProductId",
                table: "tblOrderItemEntities",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrderItemEntities");

            migrationBuilder.DropTable(
                name: "tblOrderEntities");

            migrationBuilder.DropTable(
                name: "tblOrderStatusEntities");
        }
    }
}
