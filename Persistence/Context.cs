using AuthenticationSystemApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User"); 
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Project>().ToTable("Project");

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Project>().HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Navigation(u => u.Project)
                .AutoInclude();

            modelBuilder.Entity<Person>()
                .Navigation(p => p.User)
                .AutoInclude();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Project)
                .WithMany()
                .HasForeignKey(u => u.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Person>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
