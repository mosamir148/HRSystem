using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Data
{
    public class HRDbContext:DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDbContext).Assembly);
        }
        public DbSet<Domain.Entities.Employee> Employees { get; set; }
        public DbSet<Domain.Entities.Vacation> Vacations { get; set; }
    }
}
