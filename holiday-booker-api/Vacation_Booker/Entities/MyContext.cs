using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class MyContext: IdentityDbContext<User>
    {
        //Creating a code first entity framework db with sqlserver in order to use LINQ statement in the future 
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

        }

        //Trying the override method to add the connectionstring to the context
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionstring);
        //    base.OnConfiguring(optionsBuilder);
        //}

        //a Dbset representing the tables in the database
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<UnitSizes> UnitSizes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ResortUnits> ResortUnits { get; set; }
        public DbSet<VacationSuppliers> VacationSuppliers { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}
