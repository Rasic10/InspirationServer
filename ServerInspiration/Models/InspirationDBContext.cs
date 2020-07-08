
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInspiration.Models
{
    public class InspirationDBContext : DbContext
    {
        public InspirationDBContext(DbContextOptions<InspirationDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Fevorite> Fevorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasIndex(e => e.UserName).IsUnique();
                //entity.HasMany(u => u.Songs).WithOne(s => s.User);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");
                //entity.HasOne<User>(s => s.User).WithMany(u => u.Songs).HasForeignKey(e => e.UserID);
            });

            modelBuilder.Entity<Fevorite>(entity =>
            {
                entity.ToTable("Fevorite");
                entity.HasKey(x => new { x.SongID, x.UserID });

            });

        }
    }
}
