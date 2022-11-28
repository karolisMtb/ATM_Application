using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CreditCard
    {
        internal readonly string number;
        private string _pinNumber { get; set; }
        internal double balance { get; set; }
        internal bool isCCValid { get; set; } = true;

        public CreditCard(string number, string pinNumber, double balance)
        {
            this.number = number;
            _pinNumber = pinNumber;
            this.balance = balance;
        }
    }
}
