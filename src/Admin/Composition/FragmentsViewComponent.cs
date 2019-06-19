using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Options;

namespace Admin.Composition
{
    public enum Fragments
    {
        Authorization,
        Customers
    }
    
    public class FragmentsViewComponent : ViewComponent
    {
        private readonly IOptionsSnapshot<HttpServiceOptions> httpServiceOptions;

        private static readonly Dictionary<Fragments, string> fragmentToHttpOptions = new Dictionary<Fragments, string>
        {
            {Fragments.Authorization, "AuthorizationOptions"},
            {Fragments.Customers, "CustomerOptions"}
        };

        public FragmentsViewComponent(IOptionsSnapshot<HttpServiceOptions> httpServiceOptions)
        {
            this.httpServiceOptions = httpServiceOptions;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(Fragments fragment)
        {
            var baseUrl = httpServiceOptions.Get(fragmentToHttpOptions[fragment]).BaseUrl;
            var url = new Uri(baseUrl, "/js/fragments.js");

            var scriptTag = $"<script src=\"{url.AbsoluteUri}\"></script>";
            
            return new HtmlContentViewComponentResult(new HtmlString(scriptTag));
        }
    }
}