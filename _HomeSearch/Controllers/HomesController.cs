using HomeSearch.DataAccess.CSV;
using HomeSearch.DataAccess.DAL;
using HomeSearch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSearch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomesController : Controller
    {
        private readonly HomeDB _db;

        public HomesController(IConfiguration configuration)
        {
            _db = new HomeDB(configuration.GetConnectionString("HomeSearchDB"));
        }

        // api/Homes
        [HttpGet]
        public async Task<IList<HomeModel>> GetHomes()
        {
            return await _db.GetList(null);
        }

        // api/Homes
        [HttpPost]
        public async Task ImportHomes()
        {
            var factory = new CsvFactory();
            var list = factory.ReadCSV();

            await _db.Insert(list);
            return;
        }
    }
}
