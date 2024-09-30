using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.Implementations.Offline.DataContext
{
    internal class InfoGroupDbContext(string path) : DbContextBase(path)
    {
        public DbSet<DALInfoGroup> InfoGroups { get; set; }

        public DbSet<DALTag> Tags { get; set; }
    }
}
