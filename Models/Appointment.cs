using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Клієнт")]
        public string Client { get; set; }
        [Display(Name = "Послуга")]
        public string Service { get; set; }
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        [Display(Name = "Працівник")]
        public string Worker { get; set; }
    }
}
