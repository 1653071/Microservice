

using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.Models;


namespace ModelClassLibrary.connection

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> UsersTable { get; set; }
        
    }
    

}
