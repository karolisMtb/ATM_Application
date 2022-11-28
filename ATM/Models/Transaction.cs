using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class Transaction
    {
        private Guid _id { get; set; }
        private string _customerId { get; set; }
        private string _type { get; set; }
        private DateTime _dateTime { get; set; }
        private int _ammount { get; set; }
        private string _recipient { get; set; }

        public Transaction(Guid id, string customerId, string type, DateTime dateTime, int amount, string recipient)
        {
            _id = id;
            _customerId = customerId;
            _type = type;
            _dateTime = dateTime;
            _ammount = amount;
            _recipient = recipient;
        }
    }
}
