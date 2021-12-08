using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Salon.Models;

namespace Salon.Controllers
{
    public class HomeController : DBController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Appointment()
        {
            List<Service> services = ShowServices();
            ViewBag.Services = new SelectList(services, "Id", "Name");
            return View();
        }
        public IActionResult Services(int Category)
        {
            List<Category> categories = ShowCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            List<Service> services = ShowServices();
            if (Category != 0)
            {
                return View(services.Where(s => s.Category == categories[Category - 1].Name).ToList());
            }
            return View(services);
        }
        public IActionResult Order(string fname, string lname, string phone, string email, int Service, DateTime ReleaseDate, TimeSpan appttime)
        {
            List<Client> clients = ShowClients();
            Client client = clients.Find(p => p.Name == fname && p.LastName == lname && p.Phone == phone && p.Email == email);
            List<Service> services = ShowServices();
            Service service = services.Find(p => p.Id == Service);
            List<Category> categories = ShowCategories();
            Category category = categories.Find(p => p.Name == service.Category);
            List<Worker> workers = ShowWorkers();
            Worker worker = workers.Find(p => p.Name == service.Worker);
            if (client == null)
            {
                var sql = $"insert into Salon.Клієнти(Імя, Прізвище, Телефон, Пошта) " +
                $"values ('{fname}', '{lname}', '{phone}', '{email}'); ";
                MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader MyReader;
                mySqlConnection.Open();
                MyReader = command.ExecuteReader();
                while (MyReader.Read()) { }
                mySqlConnection.Close();
                int IdClient = clients.TakeWhile((x, i) => x.Id == i + 1).LastOrDefault()?.Id + 1 ?? 1;
                var sql2 = $"insert into Salon.Запис (Дата, Клієнти_idКлієнти, Послуги_idПослуги, Послуги_Категорія_idКатегорія, Послуги_Працівники_idПрацівники) " +
                    $"values('{ReleaseDate.Year + "-" + ReleaseDate.Month + "-" + ReleaseDate.Day + " " + appttime}', '{IdClient}', '{Service}', '{category.Id}', '{worker.Id}');";
                MySqlCommand command2 = new MySqlCommand(sql2, mySqlConnection);
                MySqlDataReader MyReader2;
                mySqlConnection.Open();
                MyReader2 = command2.ExecuteReader();
                while (MyReader2.Read()) { }
                mySqlConnection.Close();
                
            }
            else
            {
                var sql2 = $"insert into Salon.Запис (Дата, Клієнти_idКлієнти, Послуги_idПослуги, Послуги_Категорія_idКатегорія, Послуги_Працівники_idПрацівники) " +
                    $"values('{ReleaseDate.Year + "-" + ReleaseDate.Month + "-" + ReleaseDate.Day + " " + appttime}', '{client.Id}', '{Service}', '{category.Id}', '{worker.Id}');";
                MySqlCommand command2 = new MySqlCommand(sql2, mySqlConnection);
                MySqlDataReader MyReader2;
                mySqlConnection.Open();
                MyReader2 = command2.ExecuteReader();
                while (MyReader2.Read()) { }
                mySqlConnection.Close();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("accesscode/admin")]
        public IActionResult AccessCode()
        {
            return View();
        }
    }
}
