using CentralisationdeDonnee.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CentralisationV0.Models.Entities
{
    public class CentralisationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Theme> Themes { get; set; }
      
        public DbSet<Industry> Industries { get; set; }
      

        public CentralisationContext() : base(nameOrConnectionString: "PostgresConnection") {
            this.Database.CommandTimeout = 120;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public static CentralisationContext Create()
        {
            return new CentralisationContext();
        }
    }
}