using enums;
using factory;
using limiter;
using model;

namespace service
{
    public class RateLimiterService
    {
        private readonly Dictionary<UserTier, RateLimiter> rateLimiters = new();

        public RateLimiterService()
        {
            rateLimiters.Add(UserTier.FREE, RateLimiterFactory.Get(RateLimitType.TOKEN_BUCKET, new RateLimitConfig(10, 60)));

            rateLimiters.Add(UserTier.PREMIUM, RateLimiterFactory.Get(RateLimitType.FIXED_WINDOW, new RateLimitConfig(100, 60)));
        }

        public bool AllowRequest(User user)
        {
            RateLimiter limiter = rateLimiters[user.Tier];
            if (limiter == null)
            {
                throw new ArgumentException("No limiter configured for tier: " + user.Tier);
            }
            return limiter.AllowRequest(user.UserId);
        }
    }
}