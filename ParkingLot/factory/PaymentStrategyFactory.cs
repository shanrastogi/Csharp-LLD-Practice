using enums;
using strategy.payment;

namespace factory
{
    public static class PaymentStrategyFactory
    {
        public static IPaymentStrategy Get(PaymentMode mode) => mode switch
        {
            PaymentMode.Cash => new CashPayment(),
            PaymentMode.Card => new CardPayment(),
            PaymentMode.Upi => new UpiPayment(),
            _ => new CashPayment()
        };
    }
}