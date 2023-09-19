using Microsoft.EntityFrameworkCore;
using PersonService.Database.Models;

namespace PersonService.Database.Context;

#nullable disable
public class PersonServiceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    
    public PersonServiceContext()
    {
        
    }

    public PersonServiceContext(DbContextOptions options): base(options)
    {
        
    }
}
#nullable restore