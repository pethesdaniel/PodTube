using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistVideoPK : Migration
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
                columns: new[] { "PlaylistId", "Index" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistVideo",
                table: "PlaylistVideo",
                columns: new[] { "PlaylistId", "VideoId", "Index" });
        }
    }
}
