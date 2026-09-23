using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.model;

namespace ATM.state
{
    public interface ATMState
    {
        void InsertCard(Card card);
        void EnterPin(string pin);
        void SelectOption(string option);
        void DispenseCash(int amount);
        void EjectCard();
        ATMStatus GetStatus();
    }
}