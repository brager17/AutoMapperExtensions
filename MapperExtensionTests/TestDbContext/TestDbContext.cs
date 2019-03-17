using System.Text.RegularExpressions;
using MapperExtensions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Tests
{
    public class TestDbContext : DbContext
    {
        public DbSet<Pupil> Pupils { get; set; }

        public TestDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
//                .UseLoggerFactory(MyLoggerFactory)
                .UseInMemoryDatabase("test");
//                .UseSqlServer("Server=.;Initial Catalog=Test;Persist Security Info=False;Integrated Security=True;");
              
            base.OnConfiguring(optionsBuilder);
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyGroup>().HasKey(x => x.Number);
            modelBuilder.Entity<IdentityCard>().HasKey(x => x.Id);
            modelBuilder.Entity<Passport>().HasKey(x => x.Number);
            modelBuilder.Entity<TIN>().HasKey(x => x.Number);
        }

        private static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] {new ConsoleLoggerProvider((_, __) => true, true)});
    }
}