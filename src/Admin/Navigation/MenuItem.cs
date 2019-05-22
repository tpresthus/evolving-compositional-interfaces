using System;

namespace Admin.Navigation
{
    public class MenuItem
    {
        public MenuItem(string title, string id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Title { get; }

        public string Id { get; }
    }
}
