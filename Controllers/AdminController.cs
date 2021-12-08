using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Salon.Models;

namespace Salon.Controllers
{
    public class AdminController : DBController
    {
        public IActionResult AdminPage(string code)
        {
            if (code == "password") return View();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AllWorkers()
        {
            List<Worker> workers = ShowWorkers();
            return View(workers);
        }
        public IActionResult AllCategories()
        {
            List<Category> categories = ShowCategories();
            return View(categories);
        }
        public IActionResult AllServices()
        {
            List<Service> services = ShowServices();
            return View(services);
        }
        public IActionResult AllClients()
        {
            List<Client> clients = ShowClients();
            return View(clients);
        }
        public IActionResult AllAppointments()
        {
            List<Appointment> appointments = ShowAppointments();
            return View(appointments);
        }
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWorker(string name, string phone)
        {
            var sql = $"insert into Salon.Працівники(ПІБ, Телефон) " +
                $"values ('{name}', '{phone}')";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()){}
            mySqlConnection.Close();
            return RedirectToAction("AllWorkers");
        }
        public IActionResult EditWorker(int id)
        {
            List<Worker> workers = ShowWorkers();
            Worker worker = workers.Find(p => p.Id == id);
            return View(worker);
        }
        [HttpPost]
        public IActionResult EditWorker(int Id, string name, string phone)
        {
            var sql = $"update Salon.Працівники " +
                $"set ПІБ = '{name}', Телефон = '{phone}' where idПрацівники = '{Id}'";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllWorkers");
        }
        public IActionResult DeleteWorker(int id)
        {
            var sql = $"delete from Salon.Працівники where idПрацівники = {id}; " +
                $"ALTER TABLE Salon.Працівники AUTO_INCREMENT = {id};";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllWorkers");
        }
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(string name, string lastname, string phone, string email)
        {
            var sql = $"insert into Salon.Клієнти(Імя, Прізвище, Телефон, Пошта) " +
                $"values ('{name}', '{lastname}', '{phone}', '{email}'); ";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllClients");
        }
        public IActionResult DeleteClient(int id)
        {
            var sql = $"delete from Salon.Клієнти where idКлієнти = {id}; " +
                $"ALTER TABLE Salon.Клієнти AUTO_INCREMENT = {id};";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllClients");
        }
        public IActionResult EditClient(int id)
        {
            List<Client> clients = ShowClients();
            Client client = clients.Find(p => p.Id == id);
            return View(client);
        }
        [HttpPost]
        public IActionResult EditClient(int Id, string name, string lastname, string phone, string email)
        {
            var sql = $"update Salon.Клієнти " +
                $"set Імя = '{name}', Прізвище = '{lastname}', Телефон = '{phone}', Пошта = '{email}' where idКлієнти = '{Id}'";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllClients");
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        public IActionResult EditCategory(int id)
        {
            List<Category> categories = ShowCategories();
            Category category = categories.Find(p => p.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult EditCategory(int Id, string category)
        {
            var sql = $"update Salon.Категорія " +
                $"set Категорія = '{category}' where idКатегорія = '{Id}'";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllCategories");
        }
        [HttpPost]
        public IActionResult AddCategory(string category)
        {
            var sql = $"insert into Salon.Категорія (Категорія) " +
                $"values ('{category}');";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllCategories");
        }
        public IActionResult DeleteCategory(int id)
        {
            var sql = $"delete from Salon.Категорія where idКатегорія = {id}; " +
                $"ALTER TABLE Salon.Категорія AUTO_INCREMENT = {id};";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllCategories");
        }
        public IActionResult AddService()
        {
            List<Category> categories = ShowCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            List<Worker> workers = ShowWorkers();
            ViewBag.Workers = new SelectList(workers, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public IActionResult AddService(string service, int price, int category, int worker)
        {
            var sql = $"insert into Salon.Послуги(Послуга, Ціна, Категорія_idКатегорія, Працівники_idПрацівники) " +
                $"values('{service}', '{price}', '{category}', '{worker}')";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllServices");
        }
        public IActionResult DeleteService(int id)
        {
            var sql = $"delete from Salon.Послуги where idПослуги = {id}; " +
                $"ALTER TABLE Salon.Послуги AUTO_INCREMENT = {id};";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllServices");
        }
        public IActionResult EditService(int id)
        {
            List<Category> categories = ShowCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            List<Worker> workers = ShowWorkers();
            ViewBag.Workers = new SelectList(workers, "Id", "Name");
            List<Service> services = ShowServices();
            Service service = services.Find(p => p.Id == id);
            return View(service);
        }
        [HttpPost]
        public IActionResult EditService(int Id, string service, int price, int category, int worker)
        {
            var sql = $"update Salon.Послуги " +
                $"set Послуга = '{service}', Ціна = '{price}', Категорія_idКатегорія = '{category}', Працівники_idПрацівники = '{worker}' where idПослуги = '{Id}'";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllServices");
        }
        public IActionResult AddAppointment()
        {
            List<Service> services = ShowServices();
            ViewBag.Services = new SelectList(services, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddAppointment(string fname, string lname, string phone, string email, int Service, DateTime ReleaseDate, TimeSpan appttime)
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
            return RedirectToAction("AllAppointments");
        }
        public IActionResult DeleteAppointment(int id)
        {
            var sql = $"delete from Salon.Запис where idЗапис = {id}; " +
                $"ALTER TABLE Salon.Запис AUTO_INCREMENT = {id};";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllAppointments");
        }
        public IActionResult EditAppointment(int id)
        {
            List<Appointment> appointments = ShowAppointments();
            Appointment appointment = appointments.Find(p => p.Id == id);
            List<Category> categories = ShowCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            List<Worker> workers = ShowWorkers();
            ViewBag.Workers = new SelectList(workers, "Id", "Name");
            List<Service> services = ShowServices();
            ViewBag.Services = new SelectList(services, "Id", "Name");
            return View(appointment);
        }
        [HttpPost]
        public IActionResult EditAppointment(int Id, DateTime ReleaseDate, TimeSpan appttime, string Client, int service)
        {
            List<Client> clients = ShowClients();
            Client client = clients.Find(p => p.Name == Client.Split(' ')[0] && p.LastName == Client.Split(' ')[1]);
            List<Service> services = ShowServices();
            Service service1 = services.Find(p => p.Id == service);
            List<Category> categories = ShowCategories();
            Category category = categories.Find(p => p.Name == service1.Category);
            List<Worker> workers = ShowWorkers();
            Worker worker = workers.Find(p => p.Name == service1.Worker);
            var sql = $"update Salon.Запис " +
                $"set Дата = '{ReleaseDate.Year + "-" + ReleaseDate.Month + "-" + ReleaseDate.Day + " " + appttime}', Клієнти_idКлієнти = '{client.Id}', Послуги_idПослуги = '{service}', Послуги_Категорія_idКатегорія = '{category.Id}', Послуги_Працівники_idПрацівники = '{worker.Id}' where idЗапис = '{Id}'";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            MySqlDataReader MyReader;
            mySqlConnection.Open();
            MyReader = command.ExecuteReader();
            while (MyReader.Read()) { }
            mySqlConnection.Close();
            return RedirectToAction("AllAppointments");
        }
    }
}