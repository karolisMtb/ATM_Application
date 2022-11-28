using ATM.Interfaces;
using ATM.Models;
using ATM.Repositories;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATM.Service
{
    public class ATMService : IATMService
    {
        private CustomerRepository customerRepository;
        public TransactionRepository transactionRepository;


        public ATMService()
        {
            customerRepository = new CustomerRepository();
            transactionRepository = new TransactionRepository();
        }
        public void StartAtmService()
        {            
            Customer customer = LogIn();
            StartUsingAtm(customer);
            // transakcijos pasirinkimas
        }

       

        public Customer LogIn()
        {
            Customer actualCustomer = new Customer();
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            bool isNameValid = ValidateNameInput(name);

            // returns customer object
            if (isNameValid)
            {
                actualCustomer = customerRepository.RetrieveCustomer(name);
            }
            else
            {
                do
                {
                    Console.WriteLine("Wrong name or non entered. Enter again: ");
                    name = Console.ReadLine();
                }
                while (ValidateNameInput(name) == false);
            }

            int ccValidationAttempts = 3;

            Console.WriteLine("Enter credit card number. You have 3 attempts: ");
            string actualCCNumber = Console.ReadLine();
            ccValidationAttempts--;
            bool isCCNumberValid = ValidateCCNumberInput(actualCCNumber, actualCustomer);
            if (isCCNumberValid)
            {
                ValidatePinNumberInput(RequestPinInput());
            }
            else
            {
                do
                {
                    Console.WriteLine($"Wrong credit card number. Enter again. You have {ccValidationAttempts} attempts: ");
                    name = Console.ReadLine();
                    ccValidationAttempts--;
                    if(ccValidationAttempts == 0)
                    {
                        ChangeCCStatus(actualCustomer, false);
                        Environment.Exit(0);
                    }
                }
                while (ValidateNameInput(name) == false && ccValidationAttempts > 0);
            }
            return actualCustomer;
        }

        private void StartUsingAtm(Customer customer)
        {
            Console.WriteLine($"What you choose: " +
                $"\nDisplay balance: 1" +
                $"\nWithdraw money: 2" +
                $"\nTransfer money: 3" +
                $"\nExit: 4");
            string transactionType = null;
            int.TryParse(Console.ReadLine(), out int selectionInput);
            switch(selectionInput){
                case 1:
                    DisplayBalance(customer);
                    break;
                case 2:
                    transactionType = "Withdrawal";
                    WithdrawMoney(customer, transactionType);
                    break;
                case 3:
                    transactionType = "Transfer";
                    TransferMoney(customer, transactionType);
                    break;
                case 4:
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Wrong input. Bye now");
                    break;
            }
        }

        public bool ValidateNameInput(string nameInput)
        {
            if(nameInput == null)
            {
                return false;
            }
            else
            {
                return customerRepository.customers.Where(x => x.name == nameInput).Any();
            }
        }

        public bool ValidateCCNumberInput(string input, Customer actualCustomer)
        {
            if (input == null)
            {
                return false;
            }
            else
            {
                return actualCustomer.creditCard.number == input; // Object reference not set to an instance of an object.'

            }
        }
        public bool ValidatePinNumberInput(string input)
        {
            return false;
        }

        public bool ChangeCCStatus(Customer thisCustomer, bool status)
        {
            customerRepository.customers.Where(x => x.id == thisCustomer.id).FirstOrDefault().creditCard.isCCValid = status;
            return status;
        }

        //TODO: pakoreguoti pin request metoda
        private string RequestPinInput()
        {
            Console.WriteLine("Enter pin number. You have 3 attempts: ");
            string pinNumber = Console.ReadLine();
            return pinNumber;
        }

        private double DisplayBalance(Customer customer)
        {
            return customerRepository.customers.FirstOrDefault(x => x.id == customer.id).creditCard.balance;
        }

        public List<Customer> WithdrawMoney(Customer thisCustomer, string transactionType)
        {
            Console.WriteLine("How much money to take out?");
            int.TryParse(Console.ReadLine(), out int withdrawAmount);

            Models.Transaction transaction = new Models.Transaction(new Guid(), thisCustomer.id, transactionType, DateTime.Now, withdrawAmount, null);

            customerRepository.customers.FirstOrDefault(x => x.id == thisCustomer.id).creditCard.balance -= withdrawAmount;
            // update DB file json
            return customerRepository.customers;
        }

        public void TransferMoney(Customer thisCustomer, string transactionType)
        {
            Console.WriteLine("Enter recipient credit card number. Format: xxxx-xxxx-xxxx-xxxx-xx");
            string ccNumber = Console.ReadLine();
            Console.WriteLine("Enter amount: ");
            int amountToTransfer = int.Parse(Console.ReadLine());

            // check balance of thisCustomer before transfering
            // if sufficient, then send, if not- OutOfRangeException
            // check if recipient exists
            try
            {
                Customer recipient = customerRepository.customers.FirstOrDefault(x => x.creditCard.number == ccNumber);
                if (thisCustomer.creditCard.balance >= amountToTransfer && recipient != null)
                {
                    Models.Transaction transaction = new Models.Transaction(new Guid(), thisCustomer.id, transactionType, DateTime.Now, amountToTransfer, ccNumber);
                    thisCustomer.creditCard.balance -= amountToTransfer;
                    recipient.creditCard.balance += amountToTransfer;
                    transactionRepository.transactions.Add(transaction);
                }

                Console.WriteLine($"ThisCustomer has { thisCustomer.creditCard.balance} euros");
                Console.WriteLine($"ThisCustomer in repository has {customerRepository.customers.FirstOrDefault(x => x.id == thisCustomer.id).creditCard.balance} euros");

            }
            // create my own Exception record not found
            //catch (RecordNotFoundException e)
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
