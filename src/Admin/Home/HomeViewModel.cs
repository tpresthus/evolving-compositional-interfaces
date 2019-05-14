using Admin.Authorization;

namespace Admin.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(User user)
        {
            if (user == null)
            {
                throw new System.ArgumentNullException(nameof(user));
            }

            AuthorizedUserName = user.Name;
        }

        public string AuthorizedUserName { get; }
    }
}