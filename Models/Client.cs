using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }
}
