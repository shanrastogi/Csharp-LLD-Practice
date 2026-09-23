using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.model;

namespace ATM.cor
{
    public interface CashDispenser
    {
        void SetNextDispenser(CashDispenser next);
        bool CanDispense(ATMModel model, int amount);
        void Dispense(ATMModel atm, int amount);
    }
}