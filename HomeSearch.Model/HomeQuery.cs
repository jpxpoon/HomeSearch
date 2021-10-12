using System;
using System.Collections.Generic;
using System.Text;

namespace HomeSearch.Model
{
    public class HomeQuery
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? Beds { get; set; }
        public decimal? Baths { get; set; }
        public long? MinSquareFeet { get; set; }
        public long? MaxSquareFeet { get; set; }
        public string PropertyTypes { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
