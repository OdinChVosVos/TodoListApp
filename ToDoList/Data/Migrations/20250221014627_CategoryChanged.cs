using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_ItemCategory_category_id",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_category_id",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "TodoItems");

            migrationBuilder.AlterColumn<long>(
                name: "category",
                table: "TodoItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_category",
                table: "TodoItems",
                column: "category");

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
                name: "FK_TodoItems_ItemCategory_category",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_category",
                table: "TodoItems");

            migrationBuilder.AlterColumn<long>(
                name: "category",
                table: "TodoItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "category_id",
                table: "TodoItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_category_id",
                table: "TodoItems",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_ItemCategory_category_id",
                table: "TodoItems",
                column: "category_id",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
