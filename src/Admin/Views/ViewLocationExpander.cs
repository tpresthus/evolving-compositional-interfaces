using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Admin.Views
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return viewLocations.Union(FeatureFolders);
        }

        private IEnumerable<string> FeatureFolders => new string[]
        {
            "/{1}/{0}" + RazorViewEngine.ViewExtension,
            "/{1}s/{0}" + RazorViewEngine.ViewExtension
        };
}
}