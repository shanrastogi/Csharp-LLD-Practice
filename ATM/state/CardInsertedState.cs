using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.model;
using ATM.service;

namespace ATM.state
{
    public class CardInsertedState : ATMState
    {
        private ATMMachine ATMMachine;

        public CardInsertedState(ATMMachine aTMMachine)
        {
            ATMMachine = aTMMachine;
        }
        public void DispenseCash(int amount)
        {
            Console.WriteLine("Enter PIN before dispensing");
        }

        public void EjectCard()
        {
            ATMMachine.Card = null;
            Console.WriteLine("Car ejected");
            ATMMachine.SetState(new IdleState(ATMMachine));
        }

        public void EnterPin(string pin)
        {
            Card? card = ATMMachine.Card;
            if (card is null)
            {
                Console.WriteLine("No card inserted.");
                return;
            }

            if (card.Pin.Equals(pin))
            {
                Console.WriteLine("PIN correct. Authenticated.");
                ATMMachine.SetState(new AuthenticatedState(ATMMachine));
            }
        }

        public ATMStatus GetStatus()
        {
            return ATMStatus.CARD_INSERTED;
        }

        public void InsertCard(Card card)
        {
            Console.WriteLine("Card already inserted");
        }

        public void SelectOption(string option)
        {
            Console.WriteLine("Select pin first");
        }
    }
}