using System.Text.Json;

namespace WeatherApp.MVC.Models
{
    public class WeatherService
    {
        private readonly WeatherOptions _options;
        public WeatherService(WeatherOptions options)
        {
            _options = options;
        }

        public async Task<WeatherApiResponse?> GetWeatherAsync(string city)
        {
            using var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://api.weatherapi.com");
            var resposnse = await _httpClient.GetAsync($"http://api.weatherapi.com/v1/forecast.json?key={_options.ApiKey}&days=10&q={city}&aqi=no");

            if (!resposnse.IsSuccessStatusCode) return null;
            var content = await resposnse.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(content);
            return weatherResponse;
        }
    }
}
