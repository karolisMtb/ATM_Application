using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public CreditCard creditCard { get; private set; }
        public List<Transaction> transactions { get; set; }

        public Customer(string id, string name, CreditCard creditCard)
        {
            this.id = id;
            this.name = name;
            this.creditCard = creditCard;
        }

        public Customer()
        {

        }
    }
}
