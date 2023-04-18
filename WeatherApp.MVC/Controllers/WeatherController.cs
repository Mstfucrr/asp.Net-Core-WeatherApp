using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.MVC.Models;

namespace WeatherApp.MVC.Controllers
{
	public class WeatherController : Controller
	{
		private WeatherOptions weatherOptions;
		private WeatherService weatherService;
		public WeatherController()
		{
			weatherOptions = new WeatherOptions
			{
				ApiKey = "d1398cba8a894feb9f7180821232602"
			};

			weatherService = new WeatherService(weatherOptions);
		}

        [HttpGet]
        public async Task<IActionResult> Index()
		{
			var weatherStatistic = await weatherService.GetWeatherAsync("London");

			if (weatherStatistic != null) ViewBag.WeatherResponse = weatherStatistic;
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(Weather weather)
        {
	        var weatherStatistic = await weatherService.GetWeatherAsync(weather.CityName);

	        if (weatherStatistic != null) ViewBag.WeatherResponse = weatherStatistic;
	        return View();
        }
    }
}
