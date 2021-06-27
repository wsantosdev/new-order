using CSharpFunctionalExtensions;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi
{
    public class AccountServiceClient : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountServiceClient(HttpClient httpClient) =>
            _httpClient = httpClient;
        
        public async ValueTask<Result> Credit(long accountNumber, decimal amount)
        {
            var requestBody = JsonSerializer.Serialize(new { AccountNumber = accountNumber, Amount = amount });
            var content = new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("api/Credit", content);
            return response.IsSuccessStatusCode
                    ? Result.Success()
                    : Result.Failure(await response.Content.ReadAsStringAsync());
        }

        public async ValueTask<Result> Debit(long accountNumber, decimal amount)
        {
            var requestBody = JsonSerializer.Serialize(new { AccountNumber = accountNumber, Amount = amount });
            var content = new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("api/Debit", content);
            return response.IsSuccessStatusCode
                    ? Result.Success()
                    : Result.Failure(await response.Content.ReadAsStringAsync());
        }
    }
}
