using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.strategy.locking
{
    public interface LockProvider
    {
        bool TryLock(string key, string userId, long ttlMs);
        void Unlock(string key);
        bool IsLockExpired(string key);
        bool IsLockedBy(string key, string userId);
    }
}