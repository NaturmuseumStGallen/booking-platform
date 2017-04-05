using System.Collections.Generic;
using System.Web.Mvc;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public class BookingController : Controller
	{
		[HttpGet]
		public ActionResult Index(int? id)
		{
			var model = new BookingModel();

			model.EventList = new List<SelectListItem>
			{
				new SelectListItem { Text = "Bitte wählen", Value = "-1" },
				new SelectListItem { Text = "Führung A", Value = "1" },
				new SelectListItem { Text = "Führung B", Value = "2" },
				new SelectListItem { Text = "Führung C", Value = "3" },
				new SelectListItem { Text = "Führung D", Value = "4" }
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(BookingModel model)
		{
			return Content("Success!");
		}
	}
}