using enums;

namespace model
{
    public class User
    {
        public string UserId { get; }
        public UserTier Tier { get; }

        public User(string _userId, UserTier _tier)
        {
            UserId = _userId;
            Tier = _tier;
        }
    }
}