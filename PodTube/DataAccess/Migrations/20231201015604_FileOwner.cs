using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FileOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "File",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_OwnerId",
                table: "File",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_AspNetUsers_OwnerId",
                table: "File",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_AspNetUsers_OwnerId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_OwnerId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "File");
        }
    }
}
