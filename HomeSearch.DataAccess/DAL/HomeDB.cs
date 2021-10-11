using HomeSearch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeSearch.DataAccess.DAL
{
    public class HomeDB : IHomeDB
    {
        private readonly HomeDBContext _context;

        public HomeDB(string connectionString)
        {
            _context = new HomeDBContext(connectionString);
        }

        public async Task<IList<HomeModel>> GetList(string key)
        {
            return await _context.Homes.ToListAsync();
        }

        public async Task Insert(IList<HomeModel> list)
        {
            _context.Homes.RemoveRange(_context.Homes);
            await _context.Homes.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
