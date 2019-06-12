using System;

namespace Customers
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException(string id)
            : base($"Customer '{id}' was not found")
        {            
        }
    }
}