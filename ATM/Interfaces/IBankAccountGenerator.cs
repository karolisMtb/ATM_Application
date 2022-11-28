using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    internal interface IBankAccountGenerator
    {
        CreditCard GenerateBankAccount();
        string GenerateCCData();
        string GenerateCCPin();
        double GenerateCCBalance();
        string GenerateCustomerId();
        string GenerateCustomerName();
    }
}
