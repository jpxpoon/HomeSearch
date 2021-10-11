using HomeSearch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeSearch.DataAccess.DAL
{
    public class HomeDBContext : DbContext
    {
        private readonly string _connectionString;

        public HomeDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<HomeModel> Homes { get; set; }
    }
}
