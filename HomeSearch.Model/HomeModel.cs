using System;

namespace HomeSearch.Model
{
    public class HomeModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public decimal Price { get; set; }
        public decimal Beds { get; set; }
        public decimal Baths { get; set; }
        public long SquareFeet { get; set; }
        public string PropertyType { get; set; }
    }
}
