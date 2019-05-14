using System;

namespace Admin.Authorization
{
    public class User
    {
        public User(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}