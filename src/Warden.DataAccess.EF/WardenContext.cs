using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Authentication;
using Microsoft.Data.Entity;

namespace Warden.DataAccess.EF

{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class WardenContext : IdentityDbContext<ApplicationUser>
    {
        public WardenContext()
        {
        }

        //public WardenContext() : base()
        //    : base("WardenContext", throwIfV1Schema: false) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasKey(l => l.UserId);
            builder.Entity<IdentityRole>().HasKey(r => r.Id);
            builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.RoleId, r.UserId });

            builder.Entity<ApplicationUser>().ToTable("Users");
            //modelBuilder.Configurations.Add(new CategoryConfiguration());
            //modelBuilder.Configurations.Add(new OrderConfiguration());
        }

        //public DbSet<Gadget> Gadgets { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<GadgetOrder> GadgetOrders { get; set; }

        public static WardenContext Create()
        {
            return new WardenContext();
        }
    }
}
