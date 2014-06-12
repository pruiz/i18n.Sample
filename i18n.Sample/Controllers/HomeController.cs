using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using i18n.Sample.Models;

namespace i18n.Sample.Controllers
{
	public partial class HomeController : LocalizableController
	{
		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Index(HomeModel model)
		{
			return View(model);
		}

	}
}
