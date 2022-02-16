using Microsoft.EntityFrameworkCore;
using netapi.Models;
namespace netapi.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }
}