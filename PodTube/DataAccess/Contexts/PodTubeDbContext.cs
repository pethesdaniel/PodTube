﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Contexts {
    public class PodTubeDbContext : IdentityDbContext<User, IdentityRole<long>, long> {
        public PodTubeDbContext(DbContextOptions<PodTubeDbContext> options) : base(options) { }

        public DbSet<Entities.Channel> Channels { get; set; }
        public DbSet<Entities.File> Files { get; set; }
        public DbSet<Entities.Playlist> Playlists { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Video> Videos { get; set; }
        public DbSet<Entities.PlaylistVideo> PlaylistVideos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Entities.Channel>().ToTable("Channel");
            modelBuilder.Entity<Entities.File>().ToTable("File");
            modelBuilder.Entity<Entities.Frame>().ToTable("Frame");
            modelBuilder.Entity<Entities.Playlist>().ToTable("Playlist");
            modelBuilder.Entity<Entities.User>().ToTable("User");
            modelBuilder.Entity<Entities.Video>().ToTable("Video");
            modelBuilder.Entity<Entities.PlaylistVideo>().ToTable("PlaylistVideo");
            base.OnModelCreating(modelBuilder);
        }
    }
}
