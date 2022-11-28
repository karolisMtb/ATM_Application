using ATM.Models;
using System.Collections.Generic;

namespace ATM.Interfaces
{
    public interface IATMService
    {
        void StartAtmService();
        //List<Customer> CreateCustomers();
        bool ValidateNameInput(string input);
        bool ValidateCCNumberInput(string input, Customer customer);
        bool ValidatePinNumberInput(string input);
        Customer LogIn();
        List<Customer> WithdrawMoney(Customer thisCustomer, string type);


    }
}
