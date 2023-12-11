using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FileExtend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserFriendlyName",
                table: "File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFriendlyName",
                table: "File");
        }
    }
}
