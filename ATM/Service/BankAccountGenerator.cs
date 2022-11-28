using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ATM.Models
{
    public class BankAccountGenerator : IBankAccountGenerator
    {
        private CreditCard _creditCard { get; set; }

        private int _creditCardNumbersLimit = 19;
        private int _creditCardNumbersLength = 16;

        public BankAccountGenerator()
        {
            
        }

        public CreditCard GenerateBankAccount()
        {
            _creditCard = new CreditCard(GenerateCCData(), GenerateCCPin(), GenerateCCBalance());
            return _creditCard;
        }

        public string GenerateCCData()
        {
            Guid guid = Guid.NewGuid();
            long  GuidInteger = 2;
            
            string str = null;

            foreach (byte b in guid.ToByteArray())
            {
                GuidInteger *= (GuidInteger * ((int)b - DateTime.Now.Millisecond));
                if(GuidInteger.ToString().Length >= 19)
                {
                    break;
                }
            }

            if (GuidInteger < 0)
            {
                GuidInteger *= (-1);
            }

            for (int k = 0; k < _creditCardNumbersLimit; k += 4)
            {
                if (k == _creditCardNumbersLength)
                {
                    str += GuidInteger.ToString().Substring(k, 2);
                }
                else
                {
                    str += GuidInteger.ToString().Substring(k, 4);
                    str += '-';
                }
            }
            return str;            
        }

        public string GenerateCCPin()
        {
            string CCNumber = GenerateCCData();
            string pinNumber = CCNumber.Substring(0, 4);
            return pinNumber;
        }

        public double GenerateCCBalance()
        {
            Random randomAccountBalance = new();
            double balance = randomAccountBalance.Next(100, 5000);
            return balance;
        }
        
        public string GenerateCustomerId()
        {
            return Guid.NewGuid().ToString().Substring(0, 4);
        }

        public string GenerateCustomerName()
        {
            string path = @"C:\Users\Karolis\source\repos\HospitalRegistry\HospitalReg\DB\Doctors.txt";
            string userName = "";
            try
            {
                int lineCount = File.ReadLines(path).Count();
                IEnumerable<string> lines = File.ReadLines(path);
                int randomNumber = new Random().Next(0, lineCount);

                if (lines.ElementAt(randomNumber) != null)
                {
                    userName = lines.ElementAt(randomNumber);
                    return userName;
                }
                else
                {
                    Console.WriteLine("Please enter your name");
                    userName = Console.ReadLine();
                    return userName;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File doesn't exist");
                Console.WriteLine(e.Message);
            }

            return userName;
        }
    }
}
