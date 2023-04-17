using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SimplifySoundToSingleAudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sound");

            migrationBuilder.RenameColumn(
                name: "SoundId",
                table: "Video",
                newName: "AudioId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_File_AudioId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_AudioId",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "AudioId",
                table: "Video",
                newName: "SoundId");

            migrationBuilder.CreateTable(
                name: "Sound",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sound_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sound_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sound_FileId",
                table: "Sound",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Sound_VideoId",
                table: "Sound",
                column: "VideoId");
        }
    }
}
