using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Contexts {
    public class PodTubeDbContext : DbContext {
        public PodTubeDbContext(DbContextOptions<PodTubeDbContext> options) : base(options) { }

        public DbSet<Entities.Channel> Channels { get; set; }
        public DbSet<Entities.File> Files { get; set; }
        public DbSet<Entities.Playlist> Playlists { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Video> Video { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Entities.Channel>().ToTable("Channel");
            modelBuilder.Entity<Entities.File>().ToTable("File");
            modelBuilder.Entity<Entities.Frame>().ToTable("Frame");
            modelBuilder.Entity<Entities.Playlist>().ToTable("Playlist").HasMany(e=>e.Videos).WithMany();
            modelBuilder.Entity<Entities.Sound>().ToTable("Sound");
            modelBuilder.Entity<Entities.User>().ToTable("User");
            modelBuilder.Entity<Entities.Video>().ToTable("Video");
        }
    }
}
