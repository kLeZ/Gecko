using System;
using System.Text;
using System.Globalization;
namespace Gecko.Extensions.StringExtensions
{
	public static class Extensions
	{
		public static string FormatWith(this string fmt, params object[] args)
		{
			return String.Format(fmt, args);
		}

		public static string FormatWith(this string fmt, IFormatProvider provider, params object[] args)
		{
			return String.Format(provider, fmt, args);
		}

		public static string RemoveDiacritics(this string s)
		{
			string normalizedString = s.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			
			for (int i = 0; i < normalizedString.Length; i++)
			{
				char c = normalizedString[i];
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
					stringBuilder.Append(c);
			}
			s = stringBuilder.ToString();
			return stringBuilder.ToString();
		}

		public static Uri ConcatWebPath(this Uri baseurl, string toConcat)
		{
			return ConcatWebPath(baseurl.ToString(), toConcat);
		}

		public static Uri ConcatWebPath(this string baseurl, string toConcat)
		{
			if (!baseurl.EndsWith("/"))
				baseurl = baseurl.Insert(baseurl.Length, "/");
			if (toConcat.StartsWith("/"))
				toConcat = toConcat.Remove(0, 1);
			Uri baseuri = new Uri(baseurl);
			Uri uri = new Uri(baseuri, toConcat);
			return uri;
		}

	}
}

