using System;

namespace Admin.Navigation
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                throw new ArgumentNullException(nameof(menuItem));
            }

            Title = menuItem.Title;
        }

        public string Title { get; }
    }
}