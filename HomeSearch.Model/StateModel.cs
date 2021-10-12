using System;
using System.ComponentModel.DataAnnotations;

namespace HomeSearch.Model
{
    public class StateModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
