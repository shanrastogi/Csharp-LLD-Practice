using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.model
{
    public class Card
    {
        public string CardNumber { get; }
        public string Pin { get; }
        public Account Account { get; }

        public Card(string _cardNumber, string _pin, Account _account)
        {
            CardNumber = _cardNumber;
            Pin = _pin;
            Account = _account;
        }
    }
}