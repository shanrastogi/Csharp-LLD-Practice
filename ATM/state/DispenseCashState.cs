using System;
using ATM.cor;
using ATM.enums;
using ATM.model;
using ATM.service;

namespace ATM.state
{
    public class DispenseCashState : ATMState
    {
        private readonly ATMMachine _atmMachine;
        private readonly CashDispenser _chain = CashDispenserChainBuilder.BuildChain();

        public DispenseCashState(ATMMachine atmMachine)
        {
            _atmMachine = atmMachine ?? throw new ArgumentNullException(nameof(atmMachine));
        }

        public void InsertCard(Card card)
        {
            Console.WriteLine("Transaction in progress. Cannot insert another card.");
        }

        public void EnterPin(string pin)
        {
            Console.WriteLine("Already authenticated.");
        }

        public void SelectOption(string option)
        {
            Console.WriteLine("Option already selected.");
        }

        public void DispenseCash(int amount)
        {
            // Assuming properties follow standard C# PascalCase or camelCase based on your model setup
            double atmBalance = _atmMachine.ATMModel.CashAvailable;
            Card? card = _atmMachine.Card;
            if (card is null)
            {
                Console.WriteLine("No card inserted.");
                return;
            }

            double accountBalance = card.Account.Balance;

            if (amount > atmBalance)
            {
                Console.WriteLine("ATM has insufficient cash. Cannot dispense " + amount);
                EjectCard();
                return;
            }

            if (amount > accountBalance)
            {
                Console.WriteLine("Insufficient account balance.");
                EjectCard();
                return;
            }

            // Now check if note combination is possible
            if (_chain.CanDispense(_atmMachine.ATMModel, amount))
            {
                _chain.Dispense(_atmMachine.ATMModel, amount);

                // Deduct from ATM cash & account balance
                _atmMachine.ATMModel.CashAvailable = atmBalance - amount;
                card.Account.Balance = accountBalance - amount;

                EjectCard();
                Console.WriteLine("Cash dispensed: " + amount);
            }
            else
            {
                Console.WriteLine("Cannot dispense requested amount with available denominations.");
                EjectCard();
            }
        }

        public void EjectCard()
        {
            _atmMachine.Card = null;
            Console.WriteLine("Card ejected.");
            _atmMachine.SetState(new IdleState(_atmMachine)); // Or use your factory if defined
        }

        public ATMStatus GetStatus()
        {
            return ATMStatus.DISPENSE_CASH;
        }
    }
}