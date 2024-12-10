using Microsoft.Extensions.Options;
using testQPD.DaData.Interfaces;

namespace testQPD.DaData
{
    
    public class DaDataClient : IDaDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly DaDataSettings _settings;

        public DaDataClient(HttpClient httpClient, IOptions<DaDataSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<AddressResponse> CleanAddressAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address cannot be null or empty.", nameof(address));
            }

            // Формируем запрос к DaData
            var response = await _httpClient.PostAsJsonAsync(string.Empty, new[] { address });

            // Обрабатываем ответ
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error calling DaData API: {response.StatusCode}, {errorContent}");
            }

            var content = await response.Content.ReadFromJsonAsync<List<AddressResponse>>();
            return content?.FirstOrDefault() ?? throw new InvalidOperationException("Invalid response from DaData API.");
        }
    }

}
