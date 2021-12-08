using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Display(Name = "Працівник")]
        public string Name { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
