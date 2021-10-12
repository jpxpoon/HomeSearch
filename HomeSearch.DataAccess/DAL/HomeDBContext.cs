using HomeSearch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeSearch.DataAccess.DAL
{
    public class HomeDBContext : DbContext
    {
        public HomeDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=HomeDB.db");
        }

        public DbSet<HomeModel> Homes { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<PropertyTypeModel> PropertyTypes { get; set; }
    }
}
