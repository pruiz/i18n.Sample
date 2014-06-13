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
			var a = _("Esto es otra cadena"
				+ " multilinea");

			var b = _(@"Y esta usa una
                  arroba");

			return View();
		}

		[HttpPost]
		public virtual ActionResult Index(HomeModel model)
		{
			return View(model);
		}

	}
}
