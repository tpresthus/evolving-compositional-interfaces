using System;

namespace Admin.Customers
{
    public class FieldViewModel
    {
        public FieldViewModel(string name, Uri key, object value)
        {
            Name = name;
            Uri = key;
            Key = key.PathAndQuery.TrimStart('/');
            Value = value;
        }
 
        public string Name { get; }
        public string Key { get; }
        public Uri Uri { get; }
        public object Value { get; }
    }
}