using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Backend.Migrations
{
    public partial class SuperiorCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SuperiorCategoryId",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SuperiorCategoryId",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
