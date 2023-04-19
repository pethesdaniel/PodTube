﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodTube.DataAccess.Contexts;

#nullable disable

namespace PodTube.DataAccess.Migrations
{
    [DbContext(typeof(PodTubeDbContext))]
    [Migration("20230412200610_PlaylistOrder")]
    partial class PlaylistOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PodTube.DataAccess.Entities.Channel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PictureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PictureId");

                    b.ToTable("Channel", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceURI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("File", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Frame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<int>("TimeStampEnd")
                        .HasColumnType("int");

                    b.Property<int>("TimeStampStart")
                        .HasColumnType("int");

                    b.Property<long?>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("VideoId");

                    b.ToTable("Frame", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Playlist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PictureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PictureId");

                    b.ToTable("Playlist", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.PlaylistVideo", b =>
                {
                    b.Property<long>("PlaylistId")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.Property<long>("Index")
                        .HasColumnType("bigint");

                    b.HasKey("PlaylistId", "VideoId", "Index");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideo", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Sound", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long?>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("VideoId");

                    b.ToTable("Sound", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProfilePictureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Video", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ChannelId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SoundId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ThumbnailId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ThumbnailId");

                    b.ToTable("Video", (string)null);
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Channel", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.User", "Owner")
                        .WithMany("Favorites")
                        .HasForeignKey("OwnerId");

                    b.HasOne("PodTube.DataAccess.Entities.File", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Owner");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Frame", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PodTube.DataAccess.Entities.Video", null)
                        .WithMany("Frames")
                        .HasForeignKey("VideoId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Playlist", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.User", "Owner")
                        .WithMany("Playlists")
                        .HasForeignKey("OwnerId");

                    b.HasOne("PodTube.DataAccess.Entities.File", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Owner");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.PlaylistVideo", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.Playlist", "Playlist")
                        .WithMany("Videos")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PodTube.DataAccess.Entities.Video", "Video")
                        .WithMany("Playlists")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Sound", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PodTube.DataAccess.Entities.Video", null)
                        .WithMany("Sound")
                        .HasForeignKey("VideoId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.User", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.File", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Video", b =>
                {
                    b.HasOne("PodTube.DataAccess.Entities.Channel", "Channel")
                        .WithMany("Videos")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PodTube.DataAccess.Entities.File", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");

                    b.Navigation("Channel");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Channel", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Playlist", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("PodTube.DataAccess.Entities.Video", b =>
                {
                    b.Navigation("Frames");

                    b.Navigation("Playlists");

                    b.Navigation("Sound");
                });
#pragma warning restore 612, 618
        }
    }
}