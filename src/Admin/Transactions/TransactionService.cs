using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
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

        public async Task<IEnumerable<Transaction>> GetTransactionsForCustomer(string customerId)
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["transactions"];
                    return jtoken
                        .Select(MapTransaction)
                        .Where(transaction => transaction.Payee.Name == customerId || transaction.Payer.Name == customerId);
                }
            }
            catch (Exception exception)
            {
                throw new TransactionException(this.baseUrl, exception);
            }
        }
        
        public async Task<Transaction> GetTransaction(int id)
        {
            try
            {
                var uri = new Uri(this.baseUrl, $"{id}");
                using (var response = await this.httpClient.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    return MapTransaction(jobject);
                }
            }
            catch (Exception exception)
            {
                throw new TransactionException(this.baseUrl, exception);
            }
        }

        public async Task<Transaction> CaptureTransaction(int transactionId, decimal amount)
        {
            try
            {
                var uri = new Uri(baseUrl, $"/{transactionId}/capture");
                var request = new {amount};

                using (var response = await httpClient.PostAsJsonAsync(uri, request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    
                    return MapTransaction(jobject);
                }
            }
            catch (Exception exception)
            {
                throw new TransactionException(this.baseUrl, exception);
            }
        }
        
        public async Task<Transaction> ReverseTransaction(int transactionId, decimal amount)
        {
            try
            {
                var uri = new Uri(baseUrl, $"/{transactionId}/reversal");
                var request = new {amount};

                using (var response = await httpClient.PostAsJsonAsync(uri, request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    
                    return MapTransaction(jobject);
                }
            }
            catch (Exception exception)
            {
                throw new TransactionException(this.baseUrl, exception);
            }
        }
        
        private static Transaction MapTransaction(JToken jtoken)
        {
            var id = jtoken["id"].Value<int>();
            var step = jtoken["step"].Value<int>();
            var type = jtoken["type"]?.ToString();
            var amount = jtoken["amount"].Value<decimal>();
            var capturedAmount = jtoken["capturedAmount"].Value<decimal>();
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
            return new Transaction(id, step, type, amount, capturedAmount, payer, payee, isFraud, isFlaggedFraud);
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
