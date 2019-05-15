using System;
using System.Collections.Generic;

namespace Admin.Navigation
{
    public class Menu
    {
        public Menu(IEnumerable<MenuItem> items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public IEnumerable<MenuItem> Items { get; }
    }
}