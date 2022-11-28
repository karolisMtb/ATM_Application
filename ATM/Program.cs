using ATM.Interfaces;
using ATM.Models;
using ATM.Repositories;
using ATM.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ATMService atmService = new ATMService();
            CustomerService customerService = new CustomerService();
            customerService.CreateCustomers();
            atmService.StartAtmService();
        }
    }
}
