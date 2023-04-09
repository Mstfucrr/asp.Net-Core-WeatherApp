using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.MVC.Controllers
{
	public class WeatherController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
