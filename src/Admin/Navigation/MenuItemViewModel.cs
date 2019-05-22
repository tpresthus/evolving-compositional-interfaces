using System;

namespace Admin.Navigation
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(MenuItem menuItem, Uri url)
        {
            if (menuItem == null)
            {
                throw new ArgumentNullException(nameof(menuItem));
            }

            Title = menuItem.Title;
            Url = url?.ToString() ?? throw new ArgumentNullException(nameof(url));
        }

        public string Title { get; }

        public string Url { get; }
    }
}
