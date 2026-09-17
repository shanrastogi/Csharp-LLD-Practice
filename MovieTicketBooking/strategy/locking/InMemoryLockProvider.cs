using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MovieTicketBooking.strategy.locking
{
    public class InMemoryLockProvider : LockProvider, IDisposable
    {
        // Internal class to hold lock metadata
        private class LockInfo
        {
            public string UserId { get; }
            public long DeadlineMs { get; }

            public LockInfo(string userId, long deadlineMs)
            {
                UserId = userId;
                DeadlineMs = deadlineMs;
            }
        }

        // Thread-safe dictionary to store locks
        private readonly ConcurrentDictionary<string, LockInfo> _locks = new();

        // Timer acting as the ScheduledExecutorService for cleanup every 1 minute
        private readonly Timer _cleanupTimer;

        public InMemoryLockProvider()
        {
            // Runs every 60 seconds (1 minute), with an initial delay of 60 seconds
            _cleanupTimer = new Timer(CleanupExpiredLocks, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public bool TryLock(string key, string userId, long ttlMs)
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long newDeadline = now + ttlMs;
            var newLock = new LockInfo(userId, newDeadline);

            while (true)
            {
                if (_locks.TryGetValue(key, out var existing))
                {
                    // If the lock is expired OR already owned by the same user (allows renewal), acquire/update it
                    if (existing.DeadlineMs <= now || existing.UserId == userId)
                    {
                        if (_locks.TryUpdate(key, newLock, existing))
                        {
                            return true;
                        }
                        // Conflict occurred, retry the loop
                        continue;
                    }

                    // Locked by someone else and still active
                    return false;
                }
                else
                {
                    // Key doesn't exist, try to add
                    if (_locks.TryAdd(key, newLock))
                    {
                        return true;
                    }
                    // Conflict occurred, retry the loop
                    continue;
                }
            }
        }

        public void Unlock(string key)
        {
            _locks.TryRemove(key, out _);
        }

        public bool IsLockExpired(string key)
        {
            if (_locks.TryGetValue(key, out var info))
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                return info.DeadlineMs <= now;
            }

            // If the key doesn't exist in the dictionary, it is effectively expired/free
            return true;
        }

        public bool IsLockedBy(string key, string userId)
        {
            if (_locks.TryGetValue(key, out var info))
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                return info.DeadlineMs > now && info.UserId == userId;
            }

            return false;
        }

        private void CleanupExpiredLocks(object? state)
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            foreach (var kvp in _locks)
            {
                // If expired, attempt to remove it safely
                if (kvp.Value.DeadlineMs <= now)
                {
                    // Ensure we only remove it if it hasn't been re-acquired/updated in the meantime
                    if (_locks.TryGetValue(kvp.Key, out var current) && current.DeadlineMs <= now)
                    {
                        _locks.TryRemove(new KeyValuePair<string, LockInfo>(kvp.Key, current));
                    }
                }
            }
        }

        public void Dispose()
        {
            // Dispose the timer to free up system resources
            _cleanupTimer?.Dispose();
        }
    }
}