using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Gecko.GenericUtils;

namespace Gecko.Extensions.CollectionExtensions
{
	public static class Extensions
	{
		public static string ToString<T>(this IEnumerable<T> root)
		{
			StringBuilder sb = new StringBuilder();
			foreach (T obj in root)
				sb.AppendLine(obj.ToString<T>());
			return sb.ToString();
		}

		public static string ToString<T>(this IEnumerable<T> root, string propseparator)
		{
			StringBuilder sb = new StringBuilder();
			foreach (T obj in root)
				sb.AppendLine(obj.ToString<T>(propseparator));
			return sb.ToString();
		}

		public static string ToString<T>(this IEnumerable<T> root, string propseparator, string fmt)
		{
			StringBuilder sb = new StringBuilder();
			foreach (T obj in root)
				sb.AppendLine(obj.ToString<T>(propseparator, fmt));
			return sb.ToString();
		}

		public static List<T> ToListOfType<T>(this IEnumerable root)
		{
			return new List<T>(root.OfType<T>());
		}

		public static void AddList<T>(this IList<T> root, IEnumerable<T> elementsToAdd)
		{
			foreach (T obj in elementsToAdd)
				root.Add(obj);
		}

		public static IEnumerable<D> Encapsulate<S, D>(this IEnumerable<S> root)
		{
			D[] ret = new D[root.Count()];
			ConstructorInfo cinfo = null;
			for (int i = 0; i < ret.Length; i++)
			{
				cinfo = typeof(D).GetConstructor(new Type[] { typeof(S) });
				ret[i] = ((D) cinfo.Invoke(new object[] { root.ElementAt(i) }));
			}
			return ret.ToList<D>().AsEnumerable();
		}

		public static void Sort<T>(this List<T> list, string sortExpression)
		{
			string[] sortExpressions = sortExpression.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			
			List<GenericComparer> comparers = new List<GenericComparer>();
			
			foreach (string sortExpress in sortExpressions)
			{
				string sortProperty = sortExpress.Trim().Split(' ')[0].Trim();
				string sortDirection = sortExpress.Trim().Split(' ')[1].Trim();
				
				PropertyInfo PropertyInfo = GenericsReflector.GetProperty<T>(sortProperty);
				SortDirection SortDirection = SortDirection.Ascending;
				if (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "ascending")
				{
					SortDirection = SortDirection.Ascending;
				}

				else if (sortDirection.ToLower() == "desc" || sortDirection.ToLower() == "descending")
					SortDirection = SortDirection.Descending;
				else
				{
					throw new Exception("Valid SortDirections are: asc, ascending, desc and descending");
				}
				
				comparers.Add(new GenericComparer { SortDirection = SortDirection, PropertyInfo = PropertyInfo, comparers = comparers });
			}
			list.Sort(comparers[0].Compare);
		}

		public static IEnumerable<T> MoveTo<T>(this IEnumerable<T> list, int From, int To, int Index)
		{
			From--;
			if (From < 0)
				From = 0;
			if (From > list.Count())
				From = list.Count();
			if (To < From)
				To = From;
			if (To > list.Count())
				To = list.Count();
			IEnumerable<T> result = list.Take(From).Concat(list.Skip(To));
			return result.Take(Index).Concat(list.Skip(From).Take(To - From)).Concat(result.Skip(Index));
		}

		///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="predicate">The expression to test the items against.</param>
		///<returns>The index of the first matching item, or -1 if no items match.</returns>
		public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
		{
			if (items == null)
				throw new ArgumentNullException("items");
			if (predicate == null)
				throw new ArgumentNullException("predicate");
			
			int retVal = 0;
			foreach (var item in items)
			{
				if (predicate(item))
					return retVal;
				retVal++;
			}
			return -1;
		}

		///<summary>Finds the index of the first occurence of an item in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="item">The item to find.</param>
		///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
		public static int IndexOf<T>(this IEnumerable<T> items, T item)
		{
			return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));
		}

		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, object> keyExtractor)
		{
			return list.Distinct<T>(new KeyEqualityComparer<T>(keyExtractor));
		}

		public static bool ListHasDifferences<T>(this IEnumerable<T> list, IEnumerable<T> otherList)
		{
			return (from item in list
				from otherItem in otherList
				select item.Equals(otherItem)).All(p => p);
		}
	}
}
