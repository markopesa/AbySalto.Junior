using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AbySalto.Junior.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Code", "DateCreated", "DateDeleted", "DateUpdated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "PENDING", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Narudžba je zaprimljena", false, "Na čekanju" },
                    { 2, "IN_PROGRESS", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Narudžba se priprema", false, "U pripremi" },
                    { 3, "COMPLETED", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Narudžba je dovršena", false, "Završena" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Code", "DateCreated", "DateDeleted", "DateUpdated", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "CASH", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Plaćanje gotovinom", false, "Gotovina" },
                    { 2, "CARD", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Plaćanje karticom", false, "Kartica" },
                    { 3, "ONLINE", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Online plaćanje", false, "Online" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ContactNumber", "Currency", "CustomerName", "DateCreated", "DateDeleted", "DateUpdated", "DeliveryAddress", "IsDeleted", "Note", "OrderStatusId", "OrderTime", "PaymentMethodId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "+385 91 123 4567", "EUR", "Marko Marković", new DateTime(2024, 10, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ilica 10, Zagreb", false, "Molim bez luka", 2, new DateTime(2024, 10, 4, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, 25.50m },
                    { 2, "+385 92 234 5678", "EUR", "Ana Anić", new DateTime(2024, 10, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Maksimirska 15, Zagreb", false, null, 1, new DateTime(2024, 10, 4, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, 42.75m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "DateCreated", "DateDeleted", "DateUpdated", "IsDeleted", "ItemName", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Pizza Margherita", 1, 12.50m, 1 },
                    { 2, new DateTime(2024, 10, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Coca Cola", 1, 6.50m, 2 },
                    { 3, new DateTime(2024, 10, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Burger Deluxe", 2, 18.00m, 1 },
                    { 4, new DateTime(2024, 10, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Pomfrit", 2, 8.00m, 2 },
                    { 5, new DateTime(2024, 10, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sprite", 2, 3.75m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
