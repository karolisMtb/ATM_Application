using ATM.Interfaces;
using ATM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATM.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> customers { get; set; }

        public CustomerRepository()
        {
            customers = new List<Customer>();
        }

        public Customer RetrieveCustomer(string name)
        {            
            Customer customer = customers.Where(x => x.name == name).First(); 

            if (customer != null)
            {
                return customer;
            }
            else
            {
                Console.WriteLine("Customer not found");
                throw new Exception();
            }
        }

        public void UpdateDBFile<T>(T item) where T : class
        {
            string path = @"C:\Users\Karolis\source\repos\ATM_Program\ATM\DB\Customers.json";
            string jsonString = JsonSerializer.Serialize<T>(item);
            
            using (JsonDocument jsonDocument = JsonDocument.Parse(jsonString))
            {
                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("creditCard");
                
                //using(JsonDocument document = JsonDocument.Parse)
           
            }
            Console.WriteLine(jsonString);
            if(!File.Exists(path))
            {
                File.Create(path);
                File.WriteAllText(path, jsonString);
            }
            else
            {
                File.WriteAllText(path, jsonString);
            }
        }
    }
}
