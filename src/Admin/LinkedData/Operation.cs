using System;

namespace Admin.LinkedData
{
    public class Operation
    {
        public Operation(Uri target, Uri type)
        {
            this.Target = target ?? throw new ArgumentNullException(nameof(target));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public Uri Target { get; }
        public Uri Type { get; }
        public Expectation Expects { get; set; }
    }
}
