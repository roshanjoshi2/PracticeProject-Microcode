using Microsoft.EntityFrameworkCore;
using PracticeProject.Models;
using System.Reflection.Emit;

namespace PracticeProject
{
    public class CCDbContext : DbContext
    {
        public CCDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hostel>().HasIndex(e => e.PAN).IsUnique();
            modelBuilder.Entity<Hostel>().Property(r => r.Email).IsRequired();
            modelBuilder.Entity<Student>().HasIndex(cit => cit.Citizenshipnumber).IsUnique();
            
        }







        public DbSet<PracticeProject.Models.Hostel>? Hostel { get; set; }


        public DbSet<PracticeProject.Models.Student>? Student { get; set; }

    }
}
