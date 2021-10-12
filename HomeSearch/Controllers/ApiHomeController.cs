using HomeSearch.DataAccess.CSV;
using HomeSearch.DataAccess.DAL;
using HomeSearch.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSearch.Controllers
{
    [ApiController]
    [Route("api/Home/[action]")]
    public class ApiHomeController : Controller
    {
        private readonly HomeDB _db;


        public ApiHomeController(IConfiguration configuration)
        {
            _db = new HomeDB(configuration.GetConnectionString("HomeSearchDB"));
        }

        // api/Home
        [HttpGet]
        public async Task<JsonResult> Homes([FromQuery] HomeQuery query)
        {
            var list = await _db.GetList(query);
            return Json(list);
        }

        // api/Home
        [HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            var factory = new CsvFactory();

            if (file.Length > 0)
            {
                using (var s = file.OpenReadStream())
                {
                    var list = factory.ReadCSV(s);
                    await _db.Insert(list);
                }
            }
          
            return Json("Success");
        }

        [HttpGet]
        public async Task<JsonResult> Cities()
        {
            var list = await _db.GetCities();
            return Json(list);
        }

        [HttpGet]
        public async Task<JsonResult> States()
        {
            var list = await _db.GetStates();
            return Json(list);
        }

        [HttpGet]
        public async Task<JsonResult> PropertyTypes()
        {
            var list = await _db.GetPropertyTypes();
            return Json(list);
        }
    }
}
