using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace i18n.Sample.Controllers
{
    public partial class LocalizationController : LocalizableController
    {
        #region Methods

        public virtual ActionResult Help(int? id)
        {
            return View();
        }

        public virtual ContentResult Index(string lang = "en", params string[] includes)
        {
			//You might need to pick the correct domain from config i.e. AppConfig.WebsiteDomain or ConfigurationManager.AppSettings["WebsiteDomain"]
			var domain = this.ControllerContext.HttpContext.Request.Url.DnsSafeHost;


            SwitchCulture(lang);
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            var sb = new StringBuilder();
            LocalizationAppConfig.LocalizationLoadComments = true;
            sb.AppendLine("var appConfig = appConfig || {};");
            sb.Append("appConfig.domain = ").Append(domain.js()).AppendLine(";");
            sb.Append("appConfig.testBaseUrl = ").Append(this.ControllerContext.HttpContext.Server.MapPath("~/app").js()).AppendLine(";");
            sb.Append("appConfig.lang = ").Append(lang.js()).AppendLine(";");
            sb.Append("appConfig.ssl = ").Append(FormsAuthentication.RequireSSL ? "true" : "false").AppendLine(";");
            sb.Append("appConfig.userInfoUrl = ").Append(Url.ActionA("Info", "User", "Account").js()).AppendLine(";");

            sb.AppendLine("appConfig.messages = {");
            foreach (var msg in I18NComplete.Localizations[lcid].Messages.Values)
                if (includes == null || includes.Length == 0 || msg.Contexts.Any(c => includes.Any(i => c.IndexOf(i, StringComparison.OrdinalIgnoreCase) >= 0)))
                {
                    sb.Append("\t").Append(msg.MsgID.js()).Append(": {");

                    if (msg.Translated || msg.HasPlural)
                    {
                        if (msg.HasPlural)
                            sb.AppendLine()
                                .Append("\t\tt: ")
                                .Append(msg.MsgStr.js())
                                .AppendLine(",")
                                .Append("\t\tp: ")
                                .Append(msg.MsgID_Plural.js())
                                .AppendLine(",")
                                .Append("\t\tpt:")
                                .Append(msg.MsgStr_Plural.js())
                                .AppendLine(",")
                                .AppendLine("\t");
                        else sb.Append("t:").Append(msg.MsgStr.js());
                    }
                    sb.AppendLine("},");
                }
            sb.AppendLine("}");

            return Content(sb.ToString(), "text/javascript");
        }

        internal static void SwitchCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        #endregion Methods of LocalizationController (3)
    }
}
