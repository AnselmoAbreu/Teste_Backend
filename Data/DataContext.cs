using Microsoft.EntityFrameworkCore;
using TesteEfx.Models;
using TesteEfx.Data;

namespace TesteEfx.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }


        public DbSet<Product> Products { get; set; }
    }
}