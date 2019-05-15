using System;

namespace Admin.Navigation
{
    public class MenuItem
    {
        public MenuItem(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
        public string Title { get; }
    }
}