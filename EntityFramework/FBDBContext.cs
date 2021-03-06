using System;
using System.Linq;
using System.Threading;
using liveBot.EntityFramework.models;
using livefb.EntityFramework.models;
using livefb.Repository.Entity;
using Microsoft.EntityFrameworkCore;
namespace liveBot.EntityFramework
{
    public class FBDBContext : DbContext
    {
        public FBDBContext(DbContextOptions<FBDBContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<StreamSesson> StreamSessons {get;set;}
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == Microsoft.EntityFrameworkCore.EntityState.Added || x.State == Microsoft.EntityFrameworkCore.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    //   string identityName = Thread.CurrentPrincipal.Identity.Name; HANDLE LATER
                    string identityName = "admin";
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}