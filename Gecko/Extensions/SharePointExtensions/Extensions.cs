using System;
namespace Gecko.Extensions.SharePointExtensions
{
	public static class Extensions
	{
		public static string GetSubsitePart(this SPContext Current, Page Page)
		{
			return GetSubsitePart(Current, Page, true);
		}

		public static string GetSubsitePart(this SPContext Current, Page Page, bool takequerystring, bool takepagename)
		{
			return GetSubsitePart(Current, Page.Request.Url.AbsoluteUri, takequerystring, takepagename);
		}

		public static string GetSubsitePart(this SPContext Current, string PageUrl, bool takequerystring, bool takepagename)
		{
			string ret = "";
			
			Uri siteUrl = new Uri(Current.Site.Url);
			Uri webUrl = new Uri(Current.Web.Url);
			Uri pageUrl = new Uri(PageUrl);
			
			if (webUrl.AbsoluteUri.StartsWith(siteUrl.AbsoluteUri))
			{
				string subset = webUrl.AbsoluteUri.Remove(0, siteUrl.AbsoluteUri.Length);
				string pathToTrim = "";
				
				if (takequerystring)
					pathToTrim = pageUrl.PathAndQuery;
				else
					pathToTrim = pageUrl.AbsolutePath;
				
				pathToTrim = pathToTrim.Substring(pathToTrim.IndexOf(subset));
				
				if (!takepagename)
					ret = pathToTrim.Substring(0, pathToTrim.LastIndexOf(pageUrl.Segments.Last()));
				else
					ret = pathToTrim;
			}
			return ret;
		}

		public static string GetSubsitePart(this SPContext Current, Page Page, bool takequerystring)
		{
			return GetSubsitePart(Current, Page.Request.Url.AbsoluteUri, takequerystring);
		}

		public static string GetSubsitePart(this SPContext Current, string PageUrl, bool takequerystring)
		{
			return GetSubsitePart(Current, PageUrl, takequerystring, true);
		}	}
}

