using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserChanged4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems",
                column: "category",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems",
                column: "category",
                principalTable: "ItemCategory",
                principalColumn: "Id");
        }
    }
}
