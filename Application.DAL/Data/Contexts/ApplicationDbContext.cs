using Application.DAL.Data.Models.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application.DAL.Data.Contexts
{
    //ApplicationDbContext inhert-> DbContext
    //ApplicationDbContext inhert-> IdentityDbContext inhert-> DbContext
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
