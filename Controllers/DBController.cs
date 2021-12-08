using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Salon.Models;

namespace Salon.Controllers
{
    public class DBController : Controller
    {
        public static string connectionString = "server=localhost;user=root;password=wowbaby04;database=Salon;";
        public MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        public List<Service> ShowServices()
        {
            var sql = "SELECT * FROM Salon.Послуги " +
                "join Категорія on Категорія_idКатегорія = Категорія.idКатегорія " +
                "join Працівники on Працівники_idПрацівники = Працівники.idПрацівники; ";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            List<Service> services = new List<Service>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    services.Add(new Service { Id = (int)rdr["idПослуги"], Name = (string)rdr["Послуга"], Price = (int)rdr["Ціна"], Category = (string)rdr["Категорія"], Worker = (string)rdr["ПІБ"]});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return services.OrderBy(p => p.Id).ToList();
        }
        public List<Category> ShowCategories()
        {
            var sql = "SELECT * FROM Salon.Категорія";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            List<Category> categories = new List<Category>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    categories.Add(new Category { Id = (int)rdr["idКатегорія"], Name = (string)rdr["Категорія"] });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return categories.OrderBy(p => p.Id).ToList();
        }
        public List<Worker> ShowWorkers()
        {
            var sql = "SELECT * FROM Salon.Працівники";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            List<Worker> workers = new List<Worker>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    workers.Add(new Worker { Id = (int)rdr["idПрацівники"], Name = (string)rdr["ПІБ"], Phone = (string)rdr["Телефон"] });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return workers.OrderBy(p => p.Id).ToList();
        }
        public List<Client> ShowClients()
        {
            var sql = "SELECT * FROM Salon.Клієнти";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            List<Client> clients = new List<Client>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    clients.Add(new Client { Id = (int)rdr["idКлієнти"], Name = (string)rdr["Імя"], LastName = (string)rdr["Прізвище"], Phone = (string)rdr["Телефон"], Email = (string)rdr["Пошта"] });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return clients.OrderBy(p => p.Id).ToList();
        }
        public List<Appointment> ShowAppointments()
        {
            var sql = "SELECT idЗапис, Дата, Імя, Прізвище, Послуга, Категорія, ПІБ FROM Salon.Запис " +
                "join Salon.Категорія on Послуги_Категорія_idКатегорія = Категорія.idКатегорія " +
                "join Salon.Працівники on Послуги_Працівники_idПрацівники = Працівники.idПрацівники " +
                "join Salon.Послуги on Послуги_idПослуги = Послуги.idПослуги " +
                "join Salon.Клієнти on Клієнти_idКлієнти = Клієнти.idКлієнти; ";
            MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                mySqlConnection.Open();
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    appointments.Add(new Appointment{ Id = (int)rdr["idЗапис"], Date = (DateTime)rdr["Дата"], Client = (string)rdr["Імя"]  + " "+ rdr["Прізвище"], Service = (string)rdr["Послуга"], Category = (string)rdr["Категорія"], Worker = (string)rdr["ПІБ"] });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return appointments.OrderBy(p => p.Id).ToList();
        }
    }
}
