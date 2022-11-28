using ATM.Interfaces;
using ATM.Models;
using ATM.Repositories;
using System;
using System.Collections.Generic;

namespace ATM.Service
{
    public class CustomerService : ICustomerService
    {
        private BankAccountGenerator bankAccountGenerator;
        private CustomerRepository customerRepository;

        public CustomerService()
        {
            bankAccountGenerator = new BankAccountGenerator();
            customerRepository = new CustomerRepository();
        }
        public List<Customer> CreateCustomers()
        {
            Console.WriteLine("How many customers would you like to create? Min 2; Max 20;");
            int.TryParse(Console.ReadLine(), out int numberOfCustomers);
            if (numberOfCustomers > 20 )
            {
                do
                {
                    ExceedRequest(numberOfCustomers);
                }
                while (numberOfCustomers > 20);
            }
            else if(numberOfCustomers <= 1 )
            {
                do
                {
                    InsufficientRequest(numberOfCustomers);
                }
                while (numberOfCustomers <= 1);
            }

            for (int i = 0; i < numberOfCustomers; i++)
            {
                Customer customer = new Customer(bankAccountGenerator.GenerateCustomerId(),
                                                bankAccountGenerator.GenerateCustomerName(),
                                               bankAccountGenerator.GenerateBankAccount());
                customerRepository.customers.Add(customer);
                customerRepository.UpdateDBFile<Customer>(customer);
            }
            return customerRepository.customers;
        }

        private void ExceedRequest(int numberOfCustomers)
        {
            Console.WriteLine("You exceeded the number of customers. Max 20. Enter again? Y/N ");
            char choice = char.Parse(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                numberOfCustomers = int.Parse(Console.ReadLine());
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void InsufficientRequest(int numberOfCustomers)
        {
            Console.WriteLine("Number is too low. Min 2. Enter again? Y/N ");
            char choice = char.Parse(Console.ReadLine());
            if (choice == 'Y' || choice == 'y')
            {
                numberOfCustomers = int.Parse(Console.ReadLine());
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
