using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data{
    public static class PrepDb{
        public static void PrepPopulation(IApplicationBuilder app, bool isProd){
            using (var serviceScope = app.ApplicationServices.CreateScope()){
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd);
            }
        }
        
        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd){
                Console.WriteLine("--> Applying migrations...");
                try{  context.Database.Migrate();}
                catch(Exception ex){
                    Console.WriteLine($"--> Could not run migrations:{ex.Message}");
                }
              
            }
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data...");
                context.Platforms.AddRange(
                    new Platform(){
                        Name= "DotNet",
                        Publisher= "Microsoft",
                        Cost="Freee"
                    }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data.");
            }
        }
    }
}