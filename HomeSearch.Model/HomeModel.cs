using System;
using System.ComponentModel.DataAnnotations;

namespace HomeSearch.Model
{
    public class HomeModel
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public long Price { get; set; }
        public decimal Beds { get; set; }
        public decimal Baths { get; set; }
        public long SquareFeet { get; set; }
        public string PropertyType { get; set; }
    }
}
