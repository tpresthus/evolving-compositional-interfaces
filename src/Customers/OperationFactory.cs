using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    public class OperationFactory
    {
        private readonly IUrlHelper urlHelper;

        public OperationFactory(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper ?? throw new System.ArgumentNullException(nameof(urlHelper));
        }

        public Operation CreateOperation(IEnumerable<Customer> customers)
        {
            var url = this.urlHelper.Action("Index", "CustomerApi", null, "http" );
            var href = new Uri(url);
            return new Operation("id", href, HttpMethod.Get);
        }

        public Operation CreateOperation(Customer customer, string actionType)
        {
            Uri target;
            HttpMethod httpMethod;
            MapLinkRelation(customer.Id,
                            actionType,
                            out target,
                            out httpMethod);
            var operation = new Operation(actionType, target, httpMethod);

            // Not very elegant, but this will do for now.
            if (actionType == "UpdateAction")
            {
                operation.Expects = new Expectation(httpMethod, "customer");
                operation.Expects.Required.Add("name");
                operation.Expects.Required.Add("PostalAddress");
                operation.Expects.Required.Add("streetAddress");
                operation.Expects.Required.Add("addressLocality");
                operation.Expects.Required.Add("postalCode");
                operation.Expects.Required.Add("addressRegion");
                operation.Expects.Required.Add("telephone");
                operation.Expects.Required.Add("birthDate");
                operation.Expects.Required.Add("email");
                operation.Expects.Required.Add("alternateName");
                operation.Expects.Required.Add("WebSite");
                operation.Expects.Required.Add("Action");
                operation.Expects.Required.Add("target");
                operation.Expects.Required.Add("httpMethod");
            }

            return operation;
        }

        private void MapLinkRelation(string id, string actionType, out Uri href, out HttpMethod httpMethod)
        {
            string controllerAction;

            switch (actionType)
            {
                case "id":
                    httpMethod = HttpMethod.Get;
                    controllerAction = "Customer";
                    break;

                case "UpdateAction":
                    httpMethod = HttpMethod.Put;
                    controllerAction = "Update";
                    break;

                default:
                    throw new ArgumentException($"Unknown action tyope '{actionType}'.", nameof(actionType));
            }

            var url = this.urlHelper.Action(controllerAction, "CustomerApi", new { id }, "http" );
            href = new Uri(url);
        }
    }
}
