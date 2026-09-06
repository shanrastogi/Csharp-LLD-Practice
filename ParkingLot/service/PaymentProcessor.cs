using enums;
using model;
using strategy.payment;

namespace service
{
    public class PaymentProcessor
    {
        private readonly IPaymentStrategy _strategy;
        public PaymentProcessor(IPaymentStrategy strategy) { _strategy = strategy; }

        public bool Pay(Ticket ticket, double amount)
        {
            bool success = _strategy.Pay(amount);
            if (success) ticket.PaymentStatus = PaymentStatus.Success;
            return success;
        }
    }
}