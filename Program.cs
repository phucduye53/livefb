using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using liveBot.EntityFramework;
using liveBot.module;
using liveBot.Repository;
using livefb.Repository;
using livefb.Services.Users;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace liveBot
{
    public class Program
    {

        public static void Main(string[] args)
        {
           
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<FBDBContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=0919061624;Database=fbdb;"));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddTransient<IUserService, UserService>();
            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //  DI magic
            var bot = ActivatorUtilities.CreateInstance<LiveBot>(serviceProvider);
     
             bot.Run();
      
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
