using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using ATM.factory;
using ATM.model;
using ATM.repository;
using ATM.state;

namespace ATM.service
{
    public class ATMMachine
    {
        public ATMModel ATMModel { get; }
        public ATMState State { get; private set; }
        public ATMRepository ATMRepository { get; }
        public Card? Card { get; set; }

        public ATMMachine(string atmId, ATMRepository atmRepository)
        {
            ATMRepository = atmRepository;
            ATMModel = ATMRepository.GetById(atmId) ?? throw new InvalidOperationException("ATM not found");
            State = ATMStateFactory.GetState(ATMModel.Status, this);
        }

        public void InsertCard(Card card)
        {
            State.InsertCard(card);
        }

        public void EnterPin(string pin)
        {
            State.EnterPin(pin);
        }

        public void SelectOption(string option)
        {
            State.SelectOption(option);
        }

        public void DispenseCash(int amount)
        {
            State.DispenseCash(amount);
        }

        public void EjectCard()
        {
            State.EjectCard();
        }

        public void SetState(ATMState state)
        {
            State = state;
            ATMModel.Status = State.GetStatus();
        }
    }
}