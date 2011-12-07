using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;

namespace Gecko.Extensions.DataExtensions
{
	public static class Extensions
	{
		public static T GetValueOrDefault<T>(this DataRow row, string colName)
		{
			T ret = default(T); int i;
			if ((i = row.Table.Columns.IndexOf(colName)) > -1)
				if (!(row[i] is DBNull)) ret = (T)row[i];
			return ret;
		}

		public static TEntity CreateEntity<TEntity>(this DataRow drData) where TEntity : class, new()
		{
			Attribute aTargetAttribute;
			Type tColumnDataType;
			TEntity targetClass = new TEntity();
			Type targetType = targetClass.GetType(); // The target object's type
			PropertyInfo[] properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			// Enumerate the properties and each property which has the XmlElementAttribute defined match the column name
			// and set the new objects value..
			foreach (PropertyInfo piTargetProperty in properties)
			{
				aTargetAttribute = Attribute.GetCustomAttribute(piTargetProperty, typeof(XmlElementAttribute));

				if (aTargetAttribute != null)
				{
					try
					{
						foreach (DataColumn column in drData.Table.Columns)
						{
							if ((aTargetAttribute as XmlElementAttribute).ElementName.ToUpper() == column.ColumnName.ToUpper())
							{
								if (drData[column.ToString()] != DBNull.Value) // Only pull over actual values
								{
									tColumnDataType = drData[column.ToString()].GetType();

									// Is the data in the database  a string format and do we
									// want a DateTime? Do the below checks and if so covert to datetime.
									if ((tColumnDataType != null) &&
										(tColumnDataType == typeof(string)) &&
										(piTargetProperty.PropertyType.IsGenericType) &&
										(piTargetProperty.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) &&
										((new NullableConverter(piTargetProperty.PropertyType)).UnderlyingType == typeof(DateTime)))
									{
										// The below pattern dd-MMM-YY is for an Oracle date target. You may need to change this depending
										// on the database being used.
										DateTime dt = DateTime.ParseExact(drData[column.ToString()].ToString(), "dd-MMM-YY", CultureInfo.CurrentCulture);
										piTargetProperty.SetValue(targetClass, dt, null);
									}
									else // Set the value which matches the property type.
										piTargetProperty.SetValue(targetClass, drData[column.ToString()], null);
								}
								break; // Column name and data associated, no need to look at the rest of the columns.
							}
						}
					}
					catch (Exception ex)
					{
						throw new ApplicationException(String.Format("Load Failure of the Attribute ({0}) for {1}. Exception:{2}", (aTargetAttribute as XmlElementAttribute).ElementName, targetType.Name, ex.Message));
					}
				}
			}
			return targetClass;
		}
	}
}
