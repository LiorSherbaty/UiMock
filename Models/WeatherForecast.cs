using System.Text.Json.Serialization;

namespace UiTesterDemo.Models
{
    // These classes are structured to match the JSON response from the Open-Meteo API.
    public class WeatherApiResponse
    {
        [JsonPropertyName("current")]
        public CurrentWeather? Current { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }
    }
}
