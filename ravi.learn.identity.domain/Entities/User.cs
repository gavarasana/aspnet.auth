using System;
using System.Collections.Generic;
using System.Text;

namespace ravi.learn.identity.domain.Entities
{
    public class User
    {
        public User(string username)
        {
            UserName = username;
        }

        public string UserName { get; private set; }
    }
}
