using System;
using System.Reflection;

namespace Gecko.GenericUtils
{
	public class GenericsReflector
	{
		public static T SetProperty<T>(T obj, string propertyName, object value)
		{
			PropertyInfo pinfo = GetProperty<T>(propertyName);
			pinfo.SetValue(obj, Convert.ChangeType(value, pinfo.PropertyType), null);
			return obj;
		}

		public static PropertyInfo GetProperty<T>(string propertyName)
		{
			Type type = typeof(T);
			PropertyInfo PropertyInfo = type.GetProperty(propertyName);
			if (PropertyInfo == null)
			{
				PropertyInfo[] props = type.GetProperties();
				foreach (PropertyInfo info in props)
				{
					if (info.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
					{
						PropertyInfo = info;
						break;
					}
				}
				if (PropertyInfo == null)
				{
					throw new Exception(String.Format("{0} is not a valid property of type: '{1}'", propertyName, type.Name));
				}
			}
			return PropertyInfo;
		}
	}
}
