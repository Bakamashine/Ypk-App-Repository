using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    StatusName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    StatusName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ypks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    YpkName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ypks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    HashPassword = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    YpkId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserInfo = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Ypks_YpkId",
                        column: x => x.YpkId,
                        principalTable: "Ypks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: false),
                    Raiting = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    YpkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatusProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductCost = table.Column<decimal>(type: "TEXT", precision: 9, scale: 2, nullable: false),
                    ProductInfo = table.Column<string>(type: "TEXT", nullable: false),
                    IsProduct = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhotoPath = table.Column<string>(type: "TEXT", nullable: true),
                    Adres = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_StatusProducts_StatusProductId",
                        column: x => x.StatusProductId,
                        principalTable: "StatusProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Ypks_YpkId",
                        column: x => x.YpkId,
                        principalTable: "Ypks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 250, nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatusOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomersComment = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    UserComment = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_StatusOrders_StatusOrderId",
                        column: x => x.StatusOrderId,
                        principalTable: "StatusOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_Id",
                table: "Feedbacks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id",
                table: "Orders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusOrderId",
                table: "Orders",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id",
                table: "Products",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StatusProductId",
                table: "Products",
                column: "StatusProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_YpkId",
                table: "Products",
                column: "YpkId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusOrders_Id",
                table: "StatusOrders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusProducts_Id",
                table: "StatusProducts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_YpkId",
                table: "Users",
                column: "YpkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ypks_Id",
                table: "Ypks",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StatusOrders");

            migrationBuilder.DropTable(
                name: "StatusProducts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Ypks");
        }
    }
}
