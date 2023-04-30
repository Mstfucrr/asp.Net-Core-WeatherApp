using HtmlAgilityPack;
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
			getNews();
		}

        [HttpGet]
        public async Task<IActionResult> Index()
		{
			_weatherApiResponse = await _weatherService.GetWeatherAsync("London");

			if (_weatherApiResponse != null) ViewBag.WeatherResponse = _weatherApiResponse;
			getNews();

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(Weather weather)
        {
	        _weatherApiResponse = await _weatherService.GetWeatherAsync(weather.CityName);

	        if (_weatherApiResponse != null) ViewBag.WeatherResponse = _weatherApiResponse;
	        ViewData["Title"] = _weatherApiResponse?.location.country + " / " + _weatherApiResponse?.location.name;
			getNews();

			return View();
        }

        private List<News> getNews()
        {
	        const string url = "https://www.mynet.com/haberler/hava-durumu";
	        var web = new HtmlWeb();
	        var doc = web.Load(url);

	        var basliklar = doc.DocumentNode.SelectNodes("//div[@class='list-item']").Take(4).ToList();
			var gorseller = doc.DocumentNode.SelectNodes("//div[@class='list-item']//img").Take(4).ToList();
			var dates = doc.DocumentNode.SelectNodes("//div[@class='list-item']//span[@class='date']").Take(4).ToList();
			var links = doc.DocumentNode.SelectNodes("//div[@class='list-item']//div[@class='card-body']//a").Take(4).ToList();
			var details = doc.DocumentNode.SelectNodes("//div[@class='list-item']//div[@class='card-body']//p").Take(4).ToList();

			var NewsList = new List<News>();
			for (var i = 0; i < basliklar.Count(); i++)
			{
				var News = new News
				{
					Baslik = basliklar[i].InnerText.Trim(),
					Image = gorseller[i].GetAttributeValue("data-original", string.Empty),
					Detail = details[i].InnerText.Trim(),
					Date = dates[i].InnerText.Trim(),
					Link = links[i].GetAttributeValue("href", string.Empty)

				};
				NewsList.Add(News);

			}
			ViewBag.NewsList = NewsList;
			return NewsList;
        }
        
	}
	public class News
	{
		public string Baslik { get; set; }
		public string Detail { get; set; }
		public string Image { get; set; }
		public string Date { get; set; }
		public string Link { get; set; }

	}
}
