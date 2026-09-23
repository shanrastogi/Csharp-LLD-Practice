using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.model;

namespace ATM.cor
{
    public class FiveHundredDispenser : CashDispenser
    {
        private CashDispenser? _next;

        public void SetNextDispenser(CashDispenser next)
        {
            _next = next;
        }

        public bool CanDispense(ATMModel model, int amount)
        {
            int availableNotes = model.fiveHundredCount; // Or model.GetFiveHundredCount() if it uses properties/methods
            int notes = Math.Min(amount / 500, availableNotes);
            int remainder = amount - notes * 500;
            return remainder == 0 || (_next != null && _next.CanDispense(model, remainder));
        }

        public void Dispense(ATMModel model, int amount)
        {
            int availableNotes = model.fiveHundredCount;
            int notes = Math.Min(amount / 500, availableNotes);
            model.fiveHundredCount = availableNotes - notes;

            int remainder = amount - notes * 500;
            if (notes > 0)
            {
                Console.WriteLine("Dispensed " + notes + " x ₹500 notes");
            }

            if (remainder > 0 && _next != null)
            {
                _next.Dispense(model, remainder);
            }
        }

    }
}