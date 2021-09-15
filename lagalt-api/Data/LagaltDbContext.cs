using lagalt_api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace lagalt_api.Data
{
    public class LagaltDbContext : DbContext
    {
        public LagaltDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users{ get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Application> Applications{ get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Project> Projects  { get; set; }
        public DbSet<Skill> Skills{ get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(SeedUsersHelper.GetUserSeeds());
            modelBuilder.Entity<Photo>().HasData(SeedUsersHelper.GetPhotoSeeds());
            modelBuilder.Entity<Project>().HasData(SeedUsersHelper.GetProjectSeeds());
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.UserId, pu.ProjectId });
            modelBuilder.Entity<User>()
                 .HasMany(u => u.SeenProjects)
                 .WithMany(p => p.SeenByUsers)
                 .UsingEntity<Dictionary<string, object>>(
                     "SeenProjectsByUser",
                     r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                     l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                     je =>
                     {
                         je.HasKey("UserId", "ProjectId");
                     });
            modelBuilder.Entity<User>()
                 .HasMany(u => u.ClickedProjects)
                 .WithMany(p => p.ClickedByUsers)
                 .UsingEntity<Dictionary<string, object>>(
                     "ClickedProjectsByUser",
                     r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                     l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                     je =>
                     {
                         je.HasKey("UserId", "ProjectId");
                     });
            modelBuilder.Entity<User>()
                 .HasMany(u => u.AppliedProjects)
                 .WithMany(p => p.AppliedByUsers)
                 .UsingEntity<Dictionary<string, object>>(
                     "AppliedProjectsByUser",
                     r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                     l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                     je =>
                     {
                         je.HasKey("UserId", "ProjectId");
                     });
            modelBuilder.Entity<User>()
                 .HasMany(u => u.ContributedProjects)
                 .WithMany(p => p.ContributedByUsers)
                 .UsingEntity<Dictionary<string, object>>(
                     "ContributedProjectsByUser",
                     r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                     l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                     je =>
                     {
                         je.HasKey("UserId", "ProjectId");
                     });
        }
    }
}
