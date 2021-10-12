using HomeSearch.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeSearch.DataAccess.CSV
{
    public class CsvFactory
    {
        public IList<HomeModel> ReadCSV(Stream s)
        {
            using (var reader = new StreamReader(s))
            {
                // get titles
                var titles = new Dictionary<string, int>();
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    titles = values.Select((v, idx) => new { v, idx }).ToDictionary(x => x.v, x => x.idx);
                }

                // read line
                var count = 0;
                var list = new List<HomeModel>();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    var model = new HomeModel
                    {
                        Id = ++count,
                        Address = values[titles["ADDRESS"]],
                        City = values[titles["CITY"]],
                        State = values[titles["STATE OR PROVINCE"]],
                        Zip = values[titles["ZIP OR POSTAL CODE"]],
                        Price = values[titles["PRICE"]] != "" ? long.Parse(values[titles["PRICE"]]) : 0,
                        Beds = values[titles["BEDS"]] != "" ? decimal.Parse(values[titles["BEDS"]]) : 0,
                        Baths = values[titles["BATHS"]] != "" ? decimal.Parse(values[titles["BATHS"]]) : 0,
                        SquareFeet = values[titles["SQUARE FEET"]] != "" ? long.Parse(values[titles["SQUARE FEET"]]) : 0,
                        PropertyType = values[titles["PROPERTY TYPE"]],
                    };

                    list.Add(model);
                }

                return list;
            }
        }
    }
}
