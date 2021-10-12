using HomeSearch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSearch.DataAccess.DAL
{
    public class HomeDB : IHomeDB
    {
        private readonly HomeDBContext _context;

        public HomeDB(string connectionString)
        {
            _context = new HomeDBContext();
        }

        public async Task<IList<HomeModel>> GetList(HomeQuery query)
        {
            var sql = "SELECT * FROM Homes WHERE 1=1";
            if (query.Beds != null)
                sql += $" AND Beds = {query.Beds.Value.ToString("0.00")}";
            if (query.Baths != null)
                sql += $" AND Baths = {query.Baths.Value.ToString("0.00")}";
            if (query.MinPrice != null)
                sql += $" AND Price >= {query.MinPrice.Value.ToString("0.00")}";
            if (query.MaxPrice != null)
                sql += $" AND Price <= {query.MaxPrice.Value.ToString("0.00")}";
            if (query.MinSquareFeet != null)
                sql += $" AND SquareFeet >= {query.MinSquareFeet.Value.ToString("0.00")}";
            if (query.MaxSquareFeet != null)
                sql += $" AND SquareFeet <= {query.MaxSquareFeet.Value.ToString("0.00")}";
            if (query.City != null)
                sql += $" AND City <= {query.City}";
            if (query.State != null)
                sql += $" AND State <= {query.State}";
            if (query.PropertyTypes != null)
                sql += $" AND PropertyType IN ({ query.PropertyTypes})";

            sql += " ORDER BY ID DESC";
            return await _context.Homes.FromSqlRaw(sql).ToListAsync();
        }

        public async Task Insert(IList<HomeModel> list)
        {
            _context.Homes.RemoveRange(_context.Homes);
            _context.Cities.RemoveRange(_context.Cities);
            _context.States.RemoveRange(_context.States);
            _context.PropertyTypes.RemoveRange(_context.PropertyTypes);
            await _context.SaveChangesAsync();

            var cities = list.Select(x => x.City).Distinct().Select((v, idx) => new CityModel { Id = idx + 1, Name = v }).ToList();
            var states = list.Select(x => x.State).Distinct().Select((v, idx) => new StateModel { Id = idx + 1, Name = v }).ToList();
            var types = list.Select(x => x.PropertyType).Distinct().Select((v, idx) => new PropertyTypeModel { Id = idx + 1, Name = v }).ToList();

            await _context.Cities.AddRangeAsync(cities);
            await _context.States.AddRangeAsync(states);
            await _context.PropertyTypes.AddRangeAsync(types);
            await _context.Homes.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<IList<CityModel>> GetCities()
        {
            return await _context.Cities.FromSqlRaw("SELECT * FROM Cities").ToListAsync();
        }
        public async Task<IList<StateModel>> GetStates()
        {
            return await _context.States.FromSqlRaw("SELECT * FROM States").ToListAsync();
        }
        public async Task<IList<PropertyTypeModel>> GetPropertyTypes()
        {
            return await _context.PropertyTypes.FromSqlRaw("SELECT * FROM PropertyTypes").ToListAsync();
        }
    }
}
