using System;
using System.Linq;
using System.Collections.Generic;

namespace Admin.Navigation
{
    public class MenuViewModel
    {
        public MenuViewModel(Menu menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            Items = menu.Items.Select(item => new MenuItemViewModel(item));
        }

        public IEnumerable<MenuItemViewModel> Items { get; }
    }
}