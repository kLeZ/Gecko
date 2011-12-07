using System.Xml;

namespace Gecko.Extensions.XmlExtensions
{
	public static class Extensions
	{
		public static int IndexOf(this XmlAttributeCollection attributes, string name, bool caseSensitive)
		{
			int ret = -1;
			for (int i = 0; i < attributes.Count; i++)
			{
				string attributeName, matchName;
				if (!caseSensitive)
				{
					attributeName = attributes[i].Name.ToLower();
					matchName = name.ToLower();
				}
				else
				{
					attributeName = attributes[i].Name;
					matchName = name;
				}

				if (attributeName.Equals(matchName))
				{
					ret = i;
					break;
				}
			}
			return ret;
		}
	}
}
