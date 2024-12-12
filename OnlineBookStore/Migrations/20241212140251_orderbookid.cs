using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookStore.Migrations
{
    /// <inheritdoc />
    public partial class orderbookid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                table: "OrderBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBooks",
                table: "OrderBooks");

            migrationBuilder.AddColumn<int>(
                name: "OrderBookId",
                table: "OrderBooks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBooks",
                table: "OrderBooks",
                column: "OrderBookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBooks_OrderId",
                table: "OrderBooks",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                table: "OrderBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                table: "OrderBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBooks",
                table: "OrderBooks");

            migrationBuilder.DropIndex(
                name: "IX_OrderBooks_OrderId",
                table: "OrderBooks");

            migrationBuilder.DropColumn(
                name: "OrderBookId",
                table: "OrderBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBooks",
                table: "OrderBooks",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                table: "OrderBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
