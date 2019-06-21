using System;

namespace Admin.Customers
{
    public class FieldViewModel
    {
        public FieldViewModel(string name, Uri key, object value)
        {
            this.Name = name;
            this.Key = key;
            this.Value = value;

        }
 
        public string Name { get; }
        public Uri Key { get; }
        public object Value { get; }
    }
}