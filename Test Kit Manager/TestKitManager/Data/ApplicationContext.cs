using Microsoft.EntityFrameworkCore;
using TestKitManager.Pages.Machines;

namespace TestKitManager.Data
{
    public class ApplicationContext : DbContext
    {
        public string DbPath { get; }

        public ApplicationContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Test Kit Manager");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DbPath = System.IO.Path.Join(path, "database.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Machine> Machines { get; set; }
    }
}