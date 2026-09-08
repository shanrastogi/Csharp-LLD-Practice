using System.Collections.Concurrent;
using enums;
using model;

namespace limiter
{
    public class SlidingWindowLogRateLimiter : RateLimiter
    {
        private readonly ConcurrentDictionary<string, Queue<long>> _requestLog = new();

        public SlidingWindowLogRateLimiter(RateLimitConfig config) : base(config, RateLimitType.SLIDING_WINDOW_LOG)
        {
        }

        public override bool AllowRequest(string userId)
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var log = _requestLog.GetOrAdd(userId, _ => new Queue<long>());

            lock (log)
            {
                while (log.Count > 0 && (now - log.Peek()) >= config.WindowInSeconds)
                {
                    log.Dequeue();
                }
                if (log.Count < config.MaxRequests)
                {
                    log.Enqueue(now);
                    return true;
                }
                return false;
            }
        }
    }
}
