using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_File_PictureId1",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_PictureId1",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                table: "Playlist");

            migrationBuilder.AlterColumn<long>(
                name: "PictureId",
                table: "Playlist",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_PictureId",
                table: "Playlist",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_File_PictureId",
                table: "Playlist",
                column: "PictureId",
                principalTable: "File",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_File_PictureId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_PictureId",
                table: "Playlist");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Playlist",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PictureId1",
                table: "Playlist",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_PictureId1",
                table: "Playlist",
                column: "PictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_File_PictureId1",
                table: "Playlist",
                column: "PictureId1",
                principalTable: "File",
                principalColumn: "Id");
        }
    }
}
