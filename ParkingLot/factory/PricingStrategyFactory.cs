using enums;
using strategy.pricing;

namespace factory
{
    public static class PricingStrategyFactory
    {
        public static IPricingStrategy Get(PricingStrategyType type) => type switch
        {
            PricingStrategyType.FlatRate => new FlatRatePricing(),
            PricingStrategyType.HourlyRate => new HourlyRatePricing(),
            _ => new HourlyRatePricing()
        };
    }
}