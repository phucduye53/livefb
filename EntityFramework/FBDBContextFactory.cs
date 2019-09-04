using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace liveBot.EntityFramework
{
    public class FBDBContextFactory: IDesignTimeDbContextFactory<FBDBContext>
    {
        public FBDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FBDBContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=0919061624;Database=fbdb;");

            return new FBDBContext(optionsBuilder.Options);
        }
    
        
    }
}