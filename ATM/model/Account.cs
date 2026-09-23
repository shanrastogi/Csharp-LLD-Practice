using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.model
{
    public class Account
    {
        public string AccountNumber { get; }
        public double Balance { get; set; }
        public Account(string _accountNumber, double balance)
        {
            AccountNumber = _accountNumber;
            Balance = balance;
        }
    }
}