using Microsoft.EntityFrameworkCore;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<FilterProcedure> FilterProcedures { get; set; }
        public DbSet<BankCustomer> BankCustomers { get; set; }
        public DbSet<Departman> Departmans { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
