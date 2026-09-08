namespace model
{
    public class RateLimitConfig
    {
        public int MaxRequests { get; }
        public int WindowInSeconds { get; }

        public RateLimitConfig(int _maxRequests, int _windowInSeconds)
        {
            MaxRequests = _maxRequests;
            WindowInSeconds = _windowInSeconds;
        }
    }
}