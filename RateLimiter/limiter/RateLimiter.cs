using enums;
using model;

namespace limiter
{
    public abstract class RateLimiter
    {
        protected readonly RateLimitConfig config;
        protected readonly RateLimitType type;

        public RateLimiter(RateLimitConfig _config, RateLimitType _type)
        {
            config = _config;
            type = _type;
        }

        public abstract bool AllowRequest(string userId);
    }
}