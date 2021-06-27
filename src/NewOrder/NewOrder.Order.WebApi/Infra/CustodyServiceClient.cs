using CSharpFunctionalExtensions;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi
{
    public class CustodyServiceClient : ICustodyService
    {
        private readonly HttpClient _httpClient;

        public CustodyServiceClient(HttpClient httpClient) =>
            _httpClient = httpClient;
        
        public async ValueTask<Result> Add(long accountNumber, string symbol, int quantity)
        {
            var requestBody = JsonSerializer.Serialize(new { AccountNumber = accountNumber, Symbol = symbol, Quantity = quantity });
            var content = new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("api/Add", content);
            return response.IsSuccessStatusCode
                    ? Result.Success()
                    : Result.Failure(await response.Content.ReadAsStringAsync());
        }

        public async ValueTask<Result> Remove(long accountNumber, string symbol, int quantity)
        {
            var requestBody = JsonSerializer.Serialize(new { AccountNumber = accountNumber, Symbol = symbol, Quantity = quantity });
            var content = new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("api/Remove", content);
            return response.IsSuccessStatusCode
                    ? Result.Success()
                    : Result.Failure(await response.Content.ReadAsStringAsync());
        }
    }
}
