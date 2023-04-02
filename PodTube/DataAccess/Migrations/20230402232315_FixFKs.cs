using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channel_File_PictureId1",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Channel_User_OwnerId1",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Frame_File_FileId1",
                table: "Frame");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_User_OwnerId1",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Sound_File_FileId1",
                table: "Sound");

            migrationBuilder.DropForeignKey(
                name: "FK_User_File_ProfilePictureId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Channel_ChannelId1",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_ChannelId1",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfilePictureId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Sound_FileId1",
                table: "Sound");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_OwnerId1",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Frame_FileId1",
                table: "Frame");

            migrationBuilder.DropIndex(
                name: "IX_Channel_OwnerId1",
                table: "Channel");

            migrationBuilder.DropIndex(
                name: "IX_Channel_PictureId1",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "ChannelId1",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FileId1",
                table: "Sound");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "FileId1",
                table: "Frame");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                table: "Channel");

            migrationBuilder.AlterColumn<long>(
                name: "SoundId",
                table: "Video",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ChannelId",
                table: "Video",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ProfilePictureId",
                table: "User",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "Sound",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Playlist",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "Frame",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PictureId",
                table: "Channel",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Channel",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_ChannelId",
                table: "Video",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePictureId",
                table: "User",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Sound_FileId",
                table: "Sound",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_OwnerId",
                table: "Playlist",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Frame_FileId",
                table: "Frame",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_OwnerId",
                table: "Channel",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_PictureId",
                table: "Channel",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_File_PictureId",
                table: "Channel",
                column: "PictureId",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_User_OwnerId",
                table: "Channel",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Frame_File_FileId",
                table: "Frame",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_User_OwnerId",
                table: "Playlist",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sound_File_FileId",
                table: "Sound",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_File_ProfilePictureId",
                table: "User",
                column: "ProfilePictureId",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Channel_ChannelId",
                table: "Video",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channel_File_PictureId",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Channel_User_OwnerId",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Frame_File_FileId",
                table: "Frame");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_User_OwnerId",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Sound_File_FileId",
                table: "Sound");

            migrationBuilder.DropForeignKey(
                name: "FK_User_File_ProfilePictureId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Channel_ChannelId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_ChannelId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfilePictureId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Sound_FileId",
                table: "Sound");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_OwnerId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Frame_FileId",
                table: "Frame");

            migrationBuilder.DropIndex(
                name: "IX_Channel_OwnerId",
                table: "Channel");

            migrationBuilder.DropIndex(
                name: "IX_Channel_PictureId",
                table: "Channel");

            migrationBuilder.AlterColumn<int>(
                name: "SoundId",
                table: "Video",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Video",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ChannelId1",
                table: "Video",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfilePictureId1",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Sound",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "FileId1",
                table: "Sound",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Playlist",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId1",
                table: "Playlist",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Frame",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "FileId1",
                table: "Frame",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Channel",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Channel",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId1",
                table: "Channel",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PictureId1",
                table: "Channel",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_ChannelId1",
                table: "Video",
                column: "ChannelId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePictureId1",
                table: "User",
                column: "ProfilePictureId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sound_FileId1",
                table: "Sound",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_OwnerId1",
                table: "Playlist",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Frame_FileId1",
                table: "Frame",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_OwnerId1",
                table: "Channel",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_PictureId1",
                table: "Channel",
                column: "PictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_File_PictureId1",
                table: "Channel",
                column: "PictureId1",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_User_OwnerId1",
                table: "Channel",
                column: "OwnerId1",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Frame_File_FileId1",
                table: "Frame",
                column: "FileId1",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_User_OwnerId1",
                table: "Playlist",
                column: "OwnerId1",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sound_File_FileId1",
                table: "Sound",
                column: "FileId1",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_File_ProfilePictureId1",
                table: "User",
                column: "ProfilePictureId1",
                principalTable: "File",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Channel_ChannelId1",
                table: "Video",
                column: "ChannelId1",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
