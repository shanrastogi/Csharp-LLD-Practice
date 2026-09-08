using System.Collections.Concurrent;
using enums;
using model;

namespace limiter
{
    public class TokenBucketRateLimiter : RateLimiter
    {
        private class BucketState
        {
            public int Tokens { get; set; }
            public long LastRefillTime { get; set; }

            public BucketState(int maxTokens, long now)
            {
                Tokens = maxTokens;
                LastRefillTime = now;
            }
        }

        private readonly ConcurrentDictionary<string, BucketState> _buckets = new();

        public TokenBucketRateLimiter(RateLimitConfig config) : base(config, RateLimitType.TOKEN_BUCKET) { }

        public override bool AllowRequest(string userId)
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var state = _buckets.GetOrAdd(userId, _ => new BucketState(config.MaxRequests, now));

            lock (state)
            {
                RefillTokens(state, now);
                if (state.Tokens > 0)
                {
                    state.Tokens--;
                    return true;
                }
                return false;
            }
        }

        private void RefillTokens(BucketState state, long now)
        {
            double secondsPerToken = (double)config.WindowInSeconds / config.MaxRequests;

            long elapsedSeconds = (now - state.LastRefillTime) / 1000;
            int tokensToAdd = (int)(elapsedSeconds / secondsPerToken);

            if (tokensToAdd > 0)
            {
                state.Tokens = Math.Min(config.MaxRequests, state.Tokens + tokensToAdd);
                state.LastRefillTime = now;
            }
        }
    }
}