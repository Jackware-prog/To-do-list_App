using Microsoft.EntityFrameworkCore;
using System.Reflection;
using To_do_list_Core.Entities;

namespace To_do_list_Infrastructure.Persistence
{
    public class To_do_list_DbContext : DbContext
    {   

        public To_do_list_DbContext(DbContextOptions options) : base(options){}

        public DbSet<List> Lists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List>().ToTable("List");
            modelBuilder.Entity<ListItem>().ToTable("ListItem");


            base.OnModelCreating(modelBuilder);
        }
    }
}
