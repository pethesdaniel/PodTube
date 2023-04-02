using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistId",
                table: "PlaylistVideo");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "PlaylistVideo",
                newName: "PlaylistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistsId",
                table: "PlaylistVideo",
                column: "PlaylistsId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistsId",
                table: "PlaylistVideo");

            migrationBuilder.RenameColumn(
                name: "PlaylistsId",
                table: "PlaylistVideo",
                newName: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistId",
                table: "PlaylistVideo",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
