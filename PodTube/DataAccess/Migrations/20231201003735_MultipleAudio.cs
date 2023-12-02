using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MultipleAudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_File_AudioId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_AudioId",
                table: "Video");

            migrationBuilder.CreateTable(
                name: "Audio",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audio_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Audio_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audio_FileId",
                table: "Audio",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Audio_VideoId",
                table: "Audio",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audio");

            migrationBuilder.CreateIndex(
                name: "IX_Video_AudioId",
                table: "Video",
                column: "AudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_File_AudioId",
                table: "Video",
                column: "AudioId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
