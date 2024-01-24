using EmpManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpManagement.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        public DbSet<Emp> Employees { get; set; } 
        
        public DbSet<Login> Logins { get; set; }
    }
}
