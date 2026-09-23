using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.model;

namespace ATM.cor
{
    public class TwoThousandDispenser : CashDispenser
    {
        private CashDispenser? Next;
        public bool CanDispense(ATMModel model, int amount)
        {
            int count = model.twoThousandCount;
            int notes = Math.Min(amount / 2000, count);
            int remainder = amount - notes * 2000;
            return remainder == 0 || (Next != null && Next.CanDispense(model, remainder));
        }

        public void Dispense(ATMModel atm, int amount)
        {
            int count = atm.twoThousandCount;
            int notes = Math.Min(amount / 2000, count);
            atm.twoThousandCount = (count - notes);

            int remainder = amount - notes * 2000;
            if (notes > 0)
                Console.WriteLine("Dispensed " + notes + " x 2000 notes");

            if (remainder > 0 && Next != null)
            {
                Next.Dispense(atm, remainder);
            }
        }

        public void SetNextDispenser(CashDispenser next)
        {
            Next = next;
        }
    }
}