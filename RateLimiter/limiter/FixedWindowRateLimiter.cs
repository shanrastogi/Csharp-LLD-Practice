using System.Collections.Concurrent;
using enums;
using model;

namespace limiter
{
    public class FixedWindowRateLimiter : RateLimiter
    {
        private class WindowState
        {
            public long WindowStart { get; set; }
            public int Count { get; set; }
        }

        private readonly ConcurrentDictionary<string, WindowState> _windows = new();

        public FixedWindowRateLimiter(RateLimitConfig config) : base(config, RateLimitType.FIXED_WINDOW) { }

        public override bool AllowRequest(string userId)
        {
            long currentReqWindow = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / config.WindowInSeconds;
            var state = _windows.GetOrAdd(userId, _ => new WindowState());

            lock (state)
            {
                if (state.WindowStart != currentReqWindow)
                {
                    Console.WriteLine($"--> WINDOW RESET at {DateTime.Now:HH:mm:ss} <--");
                    state.WindowStart = currentReqWindow;
                    state.Count = 1;
                    return true;
                }
                if (state.Count < config.MaxRequests)
                {
                    state.Count++;
                    return true;
                }
                return false;
            }
        }
    }
}