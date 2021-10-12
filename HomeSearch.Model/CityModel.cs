using System;
using System.ComponentModel.DataAnnotations;

namespace HomeSearch.Model
{
    public class CityModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
