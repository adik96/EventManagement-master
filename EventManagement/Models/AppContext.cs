using EventManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace EventsManagement.Models
{
    public class AppContext : IdentityDbContext
    {
        public AppContext() : base("DefaultConnection")
        {
        }
        public static AppContext Create()
        {
            return new AppContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User_Event> User_Event { get; set; }
        public DbSet<NtfHide> NtfHides { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>()
                .HasRequired(x => x.State)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.StateId)
                .WillCascadeOnDelete(false);

            //builder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }
}