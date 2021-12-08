using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Послуга")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public int Price { get; set; }
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        [Display(Name = "Працівник")]
        public string Worker { get; set; }
    }
}
