using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.model;
using ATM.service;

namespace ATM.state
{
    public class IdleState : ATMState
    {
        private ATMMachine ATMMachine;
        public IdleState(ATMMachine aTMMachine)
        {
            ATMMachine = aTMMachine;
        }

        public void DispenseCash(int amount)
        {
            Console.WriteLine("Co card inserted");
        }

        public void EjectCard()
        {
            Console.WriteLine("Co card inserted");
        }

        public void EnterPin(string pin)
        {
            Console.WriteLine("Co card inserted");
        }

        public ATMStatus GetStatus()
        {
            return ATMStatus.IDLE;
        }

        public void InsertCard(Card card)
        {
            ATMMachine.Card = card;
            Console.WriteLine("Card Inserted.");
            ATMMachine.SetState(new CardInsertedState(ATMMachine));
        }

        public void SelectOption(string option)
        {
            Console.WriteLine("Co card inserted");
        }
    }
}