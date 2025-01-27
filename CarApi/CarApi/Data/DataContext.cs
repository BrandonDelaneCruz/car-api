using CarApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarApi.Data
{
    public class DataContext : DbContext
    {
        // DbSets<> go here!
        public DbSet<Make> Makes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
    }
}
