using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Backend.Migrations
{
    public partial class ProductFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiles_Products_ProductId1",
                table: "ProductFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiles_ProductId1",
                table: "ProductFiles");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductFiles");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductFiles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ProductFiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiles_ProductId",
                table: "ProductFiles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiles_Products_ProductId",
                table: "ProductFiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiles_Products_ProductId",
                table: "ProductFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiles_ProductId",
                table: "ProductFiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductFiles");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ProductFiles");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductFiles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "ProductFiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiles_ProductId1",
                table: "ProductFiles",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiles_Products_ProductId1",
                table: "ProductFiles",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
