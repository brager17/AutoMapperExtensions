using MapperExtensions.Models;
using MapperExtensionTests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Tests.Models.StudioModels;

namespace Tests
{
    public class TestDbContext : DbContext
    {
        public DbSet<Pupil> Pupils { get; set; }
//        public DbSet<Studio> Studios { get; set; }
        public DbSet<Family> Families { get; set; }

        public TestDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
//                .UseLoggerFactory(MyLoggerFactory)
                .UseInMemoryDatabase("test")
                /*.UseNpgsql(
                    "Username=postgres;Password=18211921;Host=localhost;Port=5432;Database=EducationBot");*/
                ;
            base.OnConfiguring(optionsBuilder);
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityCard>().HasKey(x => x.Id);
            modelBuilder.Entity<Passport>().HasKey(x => x.Number);
            modelBuilder.Entity<TIN>().HasKey(x => x.Number);
        }

        private static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] {new ConsoleLoggerProvider((_, __) => true, true)});
    }
}