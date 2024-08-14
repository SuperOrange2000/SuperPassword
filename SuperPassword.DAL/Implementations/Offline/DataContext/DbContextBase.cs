using Microsoft.EntityFrameworkCore;

namespace SuperPassword.DAL.Implementations.Offline.DataContext
{
    internal class DbContextBase : DbContext
    {
        public string DbPath { get; init; }

        public DbContextBase(string path) : base() 
        {
            DbPath = path;
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
