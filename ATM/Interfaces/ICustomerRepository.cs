using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    internal interface ICustomerRepository
    {
        Customer RetrieveCustomer(string name);
    }
}
