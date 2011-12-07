using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Gecko.Extensions
{
	public static class ObjectExtensions
	{
		public static string ToString<T>(this T obj)
		{
			return obj.ToString<T>(", ");
		}

		public static string ToString<T>(this T obj, string propseparator, string fmt)
		{
			List<string> ret = new List<string>();
			foreach (PropertyInfo prop in typeof(T).GetProperties())
			{
				ret.Add(String.Format(fmt, prop.Name, prop.GetValue(obj, null)));
			}
			return String.Join(propseparator, ret.ToArray());
		}

		public static string ToString<T>(this T obj, string propseparator)
		{
			return ToString<T>(obj, propseparator, "{0}: '{1}'");
		}

		public static object DefaultValue(this object obj)
		{
			object ret = null;
			Type t = obj.GetType();
			if (t == typeof(bool) || t == typeof(System.Boolean)) ret = default(bool);
			else if (t == typeof(byte) || t == typeof(System.Byte)) ret = default(byte);
			else if (t == typeof(sbyte) || t == typeof(System.SByte)) ret = default(sbyte);
			else if (t == typeof(char) || t == typeof(System.Char)) ret = default(char);
			else if (t == typeof(decimal) || t == typeof(System.Decimal)) ret = default(decimal);
			else if (t == typeof(double) || t == typeof(System.Double)) ret = default(double);
			else if (t == typeof(float) || t == typeof(System.Single)) ret = default(float);
			else if (t == typeof(int) || t == typeof(System.Int32)) ret = default(int);
			else if (t == typeof(uint) || t == typeof(System.UInt32)) ret = default(uint);
			else if (t == typeof(long) || t == typeof(System.Int64)) ret = default(long);
			else if (t == typeof(ulong) || t == typeof(System.UInt64)) ret = default(ulong);
			else if (t == typeof(object) || t == typeof(System.Object)) ret = default(object);
			else if (t == typeof(short) || t == typeof(System.Int16)) ret = default(short);
			else if (t == typeof(ushort) || t == typeof(System.UInt16)) ret = default(ushort);
			else if (t == typeof(string) || t == typeof(System.String)) ret = default(string);
			return ret;
		}

		public static bool HasDifferences<T>(this T myObj, T otherObj)
		{
			bool ret = true;
			foreach (PropertyInfo prop in typeof(T).GetProperties())
				if (prop.PropertyType.IsPrimitive || prop.PropertyType.IsEnum)
					ret |= prop.GetValue(myObj, null).Equals(prop.GetValue(otherObj, null));
				else if (prop.PropertyType.IsArray)
					ret |= (from item in ((T[])prop.GetValue(myObj, null))
							from otherItem in ((T[])prop.GetValue(otherObj, null))
							select item.HasDifferences<T>(otherItem)).All(p => p);
				else ret |= prop.GetValue(myObj, null).HasDifferences(prop.GetValue(otherObj, null));
			return ret;
		}
	}
}
