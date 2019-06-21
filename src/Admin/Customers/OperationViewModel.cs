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

            Type = operation.Type.PathAndQuery.TrimStart('/');
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

        public string Type { get; }
        public IList<FieldViewModel> Required { get; }
    }
}