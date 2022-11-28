using ATM.Models;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ATM.Repositories
{
    public class TransactionRepository
    {
        public List<Transaction> transactions = new List<Transaction>();
        public TransactionRepository()
        {
            
        }
    }
}
