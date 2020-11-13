using Microsoft.EntityFrameworkCore;
namespace WebAPI.DataBase.Tables
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Disciplines> Disciplines { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Departaments> Departaments { get; set; }
        public DbSet<Setting_University> Setting_University { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-4U17TBP;Database=University_NEW;Trusted_Connection=True;");
        }

    }
}
