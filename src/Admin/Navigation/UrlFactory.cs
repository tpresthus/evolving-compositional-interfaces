using System;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Navigation
{
    public class UrlFactory
    {
        private readonly IUrlHelper urlHelper;

        public UrlFactory(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper ?? throw new System.ArgumentNullException(nameof(urlHelper));

        }

        public Uri CreateUrl(string id)
        {
            var url = MapIdToUrl(id);
            return new Uri(url);
        }

        private string MapIdToUrl(string id)
        {
            switch (id)
            {
                case "transactions":
                    return this.urlHelper.Action("Index", "Transaction", null, "http");

                case "customers":
                    return this.urlHelper.Action("Index", "Customer", null, "http");
            }

            throw new ArgumentException($"Unknown Id '{id}'.", nameof(id));
        }
    }
}
