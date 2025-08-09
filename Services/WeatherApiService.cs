using System.Net.Http;
using System.Net.Http.Json;
using UiTesterDemo.Models;

namespace UiTesterDemo.Services
{
    // The "real" service that calls the external API.
    public class WeatherApiService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> GetCurrentTemperatureAsync(double latitude, double longitude)
        {
            try
            {
                // Using string interpolation with culture-invariant formatting for lat/lon
                var requestUri = $"/v1/forecast?latitude={latitude:F2}&longitude={longitude:F2}&current=temperature_2m";
                var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(requestUri);
                return response?.Current?.Temperature ?? -999; // Return a default error value
            }
            catch (Exception ex)
            {
                // In a real app, you'd have more robust logging and error handling
                Console.WriteLine($"Error fetching weather: {ex.Message}");
                return -999;
            }
        }
    }
}
