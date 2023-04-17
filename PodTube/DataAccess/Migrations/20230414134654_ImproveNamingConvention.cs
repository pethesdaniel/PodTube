using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ImproveNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channel_File_PictureId",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_File_PictureId",
                table: "Playlist");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Playlist",
                newName: "ThumbnailId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_PictureId",
                table: "Playlist",
                newName: "IX_Playlist_ThumbnailId");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Channel",
                newName: "ThumbnailId");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_PictureId",
                table: "Channel",
                newName: "IX_Channel_ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_File_ThumbnailId",
                table: "Channel",
                column: "ThumbnailId",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_File_ThumbnailId",
                table: "Playlist",
                column: "ThumbnailId",
                principalTable: "File",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channel_File_ThumbnailId",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_File_ThumbnailId",
                table: "Playlist");

            migrationBuilder.RenameColumn(
                name: "ThumbnailId",
                table: "Playlist",
                newName: "PictureId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_ThumbnailId",
                table: "Playlist",
                newName: "IX_Playlist_PictureId");

            migrationBuilder.RenameColumn(
                name: "ThumbnailId",
                table: "Channel",
                newName: "PictureId");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_ThumbnailId",
                table: "Channel",
                newName: "IX_Channel_PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_File_PictureId",
                table: "Channel",
                column: "PictureId",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_File_PictureId",
                table: "Playlist",
                column: "PictureId",
                principalTable: "File",
                principalColumn: "Id");
        }
    }
}
