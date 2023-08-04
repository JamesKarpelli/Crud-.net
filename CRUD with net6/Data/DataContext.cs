using Microsoft.EntityFrameworkCore;

namespace CRUD_with_net6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<FootballTeams> FBTeams { get; set; }
    }
}
