using System;
using ATM.model;

namespace ATM.cor
{
    public class OneHundredDispenser : CashDispenser
    {
        private CashDispenser? _next;

        public void SetNextDispenser(CashDispenser next)
        {
            _next = next;
        }

        public bool CanDispense(ATMModel model, int amount)
        {
            int availableNotes = model.oneHundredCount; // Or model.GetOneHundredCount() depending on your model implementation
            int notes = Math.Min(amount / 100, availableNotes);
            int remainder = amount - notes * 100;

            // As this is typically the final dispenser in the chain, it returns true only if there is no remainder
            return remainder == 0;
        }

        public void Dispense(ATMModel model, int amount)
        {
            int availableNotes = model.oneHundredCount;
            int notes = Math.Min(amount / 100, availableNotes);
            model.oneHundredCount = availableNotes - notes;

            if (notes > 0)
            {
                Console.WriteLine("Dispensed " + notes + " x ₹100 notes");
            }
        }
    }
}