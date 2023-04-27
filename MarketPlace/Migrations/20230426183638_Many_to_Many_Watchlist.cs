using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class Many_to_Many_Watchlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_RegisteredUserId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "UsersProductsWatchlist",
                columns: table => new
                {
                    RegisteredUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProductsWatchlist", x => new { x.RegisteredUserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_UsersProductsWatchlist_AspNetUsers_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersProductsWatchlist_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersProductsWatchlist_ProductId",
                table: "UsersProductsWatchlist",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_RegisteredUserId",
                table: "Products",
                column: "RegisteredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_RegisteredUserId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "UsersProductsWatchlist");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_RegisteredUserId",
                table: "Products",
                column: "RegisteredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
