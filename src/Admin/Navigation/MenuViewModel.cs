using System;
using System.Linq;
using System.Collections.Generic;

namespace Admin.Navigation
{
    public class MenuViewModel
    {
        private readonly UrlFactory urlFactory;

        public MenuViewModel(Menu menu, UrlFactory urlFactory)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            this.urlFactory = urlFactory ?? throw new ArgumentNullException(nameof(urlFactory));
            Items = menu.Items.Select(MapMenuItem);
        }

        public IEnumerable<MenuItemViewModel> Items { get; }


        private MenuItemViewModel MapMenuItem(MenuItem menuItem)
        {
            var url = this.urlFactory.CreateUrl(menuItem.Id);
            return new MenuItemViewModel(menuItem, url);
        }
    }
}
