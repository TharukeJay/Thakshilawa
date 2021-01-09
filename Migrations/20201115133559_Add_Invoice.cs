using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XYZLaundry.Migrations
{
    public partial class Add_Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    NoteToRecipient = table.Column<string>(nullable: true),
                    TermsAndCondition = table.Column<string>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Shipping = table.Column<decimal>(nullable: false),
                    GrandTotal = table.Column<decimal>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
