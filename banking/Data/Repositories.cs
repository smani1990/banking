using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Data
{
    public class DBRepositories : DbContext
    {
        //constructor
        public DBRepositories(DbContextOptions<DBRepositories> options) : base(options)
        {
        }
        //Dbset property
        public DbSet<Account> account { get; set; }
        public DbSet<User> user { get; set; }
     


    }

}
