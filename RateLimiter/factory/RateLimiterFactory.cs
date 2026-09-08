using enums;
using limiter;
using model;

namespace factory
{
    public static class RateLimiterFactory
    {
        public static RateLimiter Get(RateLimitType algo, RateLimitConfig config) => algo switch
        {
            RateLimitType.TOKEN_BUCKET => new TokenBucketRateLimiter(config),
            RateLimitType.FIXED_WINDOW => new FixedWindowRateLimiter(config),
            RateLimitType.SLIDING_WINDOW_LOG => new SlidingWindowLogRateLimiter(config),
            _ => throw new ArgumentException("Invalid algo type")
        };

    }
}