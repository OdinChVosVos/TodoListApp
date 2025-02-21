using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserChanged3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCategory_AspNetUsers_UserId",
                table: "ItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_AspNetUsers_UserId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoItems",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ItemCategory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategory_AspNetUsers_UserId",
                table: "ItemCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_AspNetUsers_UserId",
                table: "TodoItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems",
                column: "category",
                principalTable: "ItemCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCategory_AspNetUsers_UserId",
                table: "ItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_AspNetUsers_UserId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoItems",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ItemCategory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategory_AspNetUsers_UserId",
                table: "ItemCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_AspNetUsers_UserId",
                table: "TodoItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems",
                column: "category",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
