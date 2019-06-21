using System;
using System.Collections.Generic;
using System.Linq;
using Admin.LinkedData;

namespace Admin.Customers
{
    public class OperationViewModel
    {
        public OperationViewModel(LinkedDataObject data, Operation operation)
        {
            if (data is null)
            {
                throw new System.ArgumentNullException(nameof(data));
            }

            if (operation is null)
            {
                throw new System.ArgumentNullException(nameof(operation));
            }

            Name = operation.Type.PathAndQuery.TrimStart('/');
            Type = operation.Type;
            Method = operation.Expects.Method.Method;
            Target = operation.Target;
            Required = new List<FieldViewModel>();
            
            if (operation.Expects.Required.Any())
            {
                foreach (var required in operation.Expects.Required)
                {
                    var key = data.Context.Expand(required);
                    var name = LinkedDataObject.MapKey(key);
                    if (!data.ContainsKey(key))
                    {
                        continue;
                    }
                    
                    var value = data[key];
                    var field = new FieldViewModel(name, key, value);
                    Required.Add(field);
                }
            }
        }

        public Uri Target { get; }
        public string Method { get; }
        public string Name { get; }
        public Uri Type { get; }
        public IList<FieldViewModel> Required { get; }
    }
}