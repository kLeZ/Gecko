using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Reflection;

namespace Gecko.Extensions.WebExtensions
{
	public static class Extensions
	{
		public static UserControl LoadControl(this Page Page, string UserControlPath, params object[] constructorParameters)
		{
			List<Type> constParamTypes = new List<Type>();
			foreach (object constParam in constructorParameters) {
				constParamTypes.Add(constParam.GetType());
			}
			
			UserControl ctl = Page.LoadControl(UserControlPath) as UserControl;
			
			// Find the relevant constructor
			ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());
			
			//And then call the relevant constructor
			if (constructor == null) {
				throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
			} else {
				constructor.Invoke(ctl, constructorParameters);
			}
			
			// Finally return the fully initialized UC
			return ctl;
		}

		public static T GetHeaderValue<T>(string key)
		{
			T ret = default(T);
			HttpRequest request = HttpContext.Current.Request;
			ret = (T)((object)request.Headers[key]);
			return ret;
		}

		public static void Redirect(this HttpResponse response, string url, string target, string windowFeatures)
		{
			if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures)) {
				response.Redirect(url);
			} else {
				Page page = HttpContext.Current.Handler as Page;
				if (page == null) {
					throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
				}
				url = page.ResolveClientUrl(url);
				string script;
				if (!String.IsNullOrEmpty(windowFeatures)) {
					script = "window.open(\"{0}\", \"{1}\", \"{2}\");";
				} else {
					script = "window.open(\"{0}\", \"{1}\");";
				}
				
				script = String.Format(script, url, target, windowFeatures);
				ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
			}
		}
	}
}
