using System;

namespace Admin.Authorization
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Name = user.Name;
        }

        public string Name { get; }
    }
}