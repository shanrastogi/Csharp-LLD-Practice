using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.model;
using ATM.service;

namespace ATM.state
{
    public class AuthenticatedState : ATMState
    {
        private ATMMachine ATMMachine;

        public AuthenticatedState(ATMMachine aTMMachine)
        {
            ATMMachine = aTMMachine;
        }

        public void DispenseCash(int amount)
        {
            Console.WriteLine("Select an option first.");
        }

        public void EjectCard()
        {
            ATMMachine.Card = null;
            Console.WriteLine("Card ejected.");
            ATMMachine.SetState(new IdleState(ATMMachine));
        }

        public void EnterPin(string pin)
        {
            Console.WriteLine("Already authenticated.");
        }

        public ATMStatus GetStatus()
        {
            return ATMStatus.AUTHENTICATED;
        }

        public void InsertCard(Card card)
        {
            Console.WriteLine("Card already inserted.");
        }

        public void SelectOption(string option)
        {
            Console.WriteLine("Option selected: Withdrawal.");
            ATMMachine.SetState(new DispenseCashState(ATMMachine));
        }
    }
}