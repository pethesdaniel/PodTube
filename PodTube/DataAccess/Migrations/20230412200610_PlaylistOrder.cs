using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PlaylistOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistsId",
                table: "PlaylistVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Video_VideosId",
                table: "PlaylistVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistVideo_VideosId",
                table: "PlaylistVideo");

            migrationBuilder.RenameColumn(
                name: "VideosId",
                table: "PlaylistVideo",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "PlaylistsId",
                table: "PlaylistVideo",
                newName: "VideoId");

            migrationBuilder.AddColumn<long>(
                name: "PlaylistId",
                table: "PlaylistVideo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistId", "VideoId", "Index" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideo_VideoId",
                table: "PlaylistVideo",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistId",
                table: "PlaylistVideo",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Video_VideoId",
                table: "PlaylistVideo",
                column: "VideoId",
                principalTable: "Video",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistId",
                table: "PlaylistVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistVideo_Video_VideoId",
                table: "PlaylistVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistVideo_VideoId",
                table: "PlaylistVideo");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "PlaylistVideo");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "PlaylistVideo",
                newName: "VideosId");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "PlaylistVideo",
                newName: "PlaylistsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistsId", "VideosId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideo_VideosId",
                table: "PlaylistVideo",
                column: "VideosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Playlist_PlaylistsId",
                table: "PlaylistVideo",
                column: "PlaylistsId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistVideo_Video_VideosId",
                table: "PlaylistVideo",
                column: "VideosId",
                principalTable: "Video",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
