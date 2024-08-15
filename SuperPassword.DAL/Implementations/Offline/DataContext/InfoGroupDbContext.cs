using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Implementations.Models;

namespace SuperPassword.DAL.Implementations.Offline.DataContext
{
    internal class InfoGroupDbContext(string path) : DbContextBase(path)
    {
        public DbSet<DALInfoGroup> InfoGroups { get; set; }

        public DbSet<DALTag> Tags { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DALInfoGroup>()
        //        .HasMany(e => e.Tags)
        //        .WithOne(e => e.InfoGroup)
        //        .HasForeignKey(e => e.InfoGroupId)
        //        .HasPrincipalKey(e => e.Id);
        //}
    }
}
