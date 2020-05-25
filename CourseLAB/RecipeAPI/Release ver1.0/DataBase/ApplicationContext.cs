using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace DATABASE
{
    public class ApplicationContext : DbContext
    {
        public DbSet<dataBaseUser> Users { get; set; }
        public DbSet<dataBaseRecipe> Recipes { get;set; }
         
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }


    }
}
