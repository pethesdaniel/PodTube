using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistVideo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistId", "VideoId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideo_PlaylistId_Index",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistId", "Index" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistVideo_PlaylistId_Index",
                table: "PlaylistVideo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistId", "Index" });
        }
    }
}
