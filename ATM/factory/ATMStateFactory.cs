using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.service;
using ATM.state;

namespace ATM.factory
{
    public static class ATMStateFactory
    {
        public static ATMState GetState(ATMStatus status, ATMMachine machine) => status switch
        {
            ATMStatus.AUTHENTICATED => new AuthenticatedState(machine),
            ATMStatus.CARD_INSERTED => new CardInsertedState(machine),
            ATMStatus.DISPENSE_CASH => new DispenseCashState(machine),
            ATMStatus.IDLE => new IdleState(machine),
            _ => throw new ArgumentException("Invalid ATM status")
        };
    }
}