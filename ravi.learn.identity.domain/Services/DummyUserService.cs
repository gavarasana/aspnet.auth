using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ravi.learn.identity.domain.Entities;

namespace ravi.learn.identity.domain.Services
{
    public class DummyUserService : IUserService
    {
        private IDictionary<string, (string PasswordHash, User user)> _users =
                new Dictionary<string, (string PasswordHash, User user)>();



        public DummyUserService(IDictionary<string,string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
            }
        }

        public Task<bool> ValidateCredentials(string userName, string password, out User user)
        {
            user = null;
            var key = userName.ToLower();
            if (_users.ContainsKey(key))
            {
                var hashedPassword = _users[key].PasswordHash;
                if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                {
                    user = _users[key].user;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
