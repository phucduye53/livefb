using liveBot.EntityFramework.models;
using Microsoft.EntityFrameworkCore;

namespace liveBot.EntityFramework
{
    public class FBDBContext:DbContext
    {
         public FBDBContext(DbContextOptions<FBDBContext> options)
        : base(options)
         {
         }
 
         public DbSet<User> Users { get; set; }
 
         public DbSet<Comment> Comments { get; set; }
         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}