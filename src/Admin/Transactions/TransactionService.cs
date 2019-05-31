using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Admin.Transactions
{
    public class TransactionService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUrl;

        public TransactionService(HttpClient httpClient, IOptionsSnapshot<HttpServiceOptions> httpServiceOptions)
        {
            if (httpServiceOptions == null)
            {
                throw new ArgumentNullException(nameof(httpServiceOptions));
            }

            this.baseUrl = httpServiceOptions.Get("TransactionOptions").BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["transactions"];
                    return jtoken.Select(MapTransaction);
                }
            }
            catch (Exception exception)
            {
                throw new TransactionException(this.baseUrl, exception);
            }
        }

        private static Transaction MapTransaction(JToken jtoken)
        {
            var step = jtoken["step"].Value<int>();
            var type = jtoken["type"]?.ToString();
            var amount = jtoken["amount"].Value<decimal>();
            var payerToken = jtoken["payer"];
            var payerName = payerToken["name"]?.ToString();
            var payerOldBalance = payerToken["oldBalance"].Value<decimal>();
            var payerNewBalance = payerToken["newBalance"].Value<decimal>();
            var payer = new Party(payerName, payerOldBalance, payerNewBalance);
            var payeeToken = jtoken["payee"];
            var payeeName = payeeToken["name"]?.ToString();
            var payeeOldBalance = payeeToken["oldBalance"].Value<decimal>();
            var payeeNewBalance = payeeToken["newBalance"].Value<decimal>();
            var payee = new Party(payeeName, payeeOldBalance, payeeNewBalance);
            var isFraud = jtoken["isFraud"].Value<bool>();
            var isFlaggedFraud = jtoken["isFlaggedFraud"].Value<bool>();
            return new Transaction(step, type, amount, payer, payee, isFraud, isFlaggedFraud);
        }

        private class TransactionException : ApplicationException
        {
            public TransactionException(Uri requestUrl, Exception innerException)
                : base($"The transaction retrieval request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}
