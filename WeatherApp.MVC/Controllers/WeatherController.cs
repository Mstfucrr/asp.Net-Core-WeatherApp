using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.MVC.Models;

namespace WeatherApp.MVC.Controllers
{
	public class WeatherController : Controller
	{
		private readonly WeatherOptions _weatherOptions;
		private readonly WeatherService _weatherService;
		private WeatherApiResponse? _weatherApiResponse;
		public WeatherController()
		{
			_weatherOptions = new WeatherOptions
			{
				ApiKey = "d1398cba8a894feb9f7180821232602"
			};
			_weatherApiResponse = new WeatherApiResponse();
			_weatherService = new WeatherService(_weatherOptions);
		}

        [HttpGet]
        public async Task<IActionResult> Index()
		{
			_weatherApiResponse = await _weatherService.GetWeatherAsync("London");

			if (_weatherApiResponse != null) ViewBag.WeatherResponse = _weatherApiResponse;
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(Weather weather)
        {
	        _weatherApiResponse = await _weatherService.GetWeatherAsync(weather.CityName);

	        if (_weatherApiResponse != null) ViewBag.WeatherResponse = _weatherApiResponse;
	        
	        return View();
        }
    }
}
