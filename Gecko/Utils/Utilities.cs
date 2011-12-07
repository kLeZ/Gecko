using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Gecko.Extensions;

namespace Gecko.Utils
{
	public class Utilities
	{
		private const string PBYTE = "PB", TBYTE = "TB", GBYTE = "GB", MBYTE = "MB", KBYTE = "KB";

		/// <summary>
		/// Spezza un file di testo in più parti
		/// </summary>
		/// <param name="filePath">Percorso del file di origine</param>
		/// <param name="numRecordPerFile">Numero di righe del file di origine</param>
		/// <param name="fileNamePattern">Pattern del nome dei part di destinazione, richiede la presenza di tre segnaposto (come nella funzione Format <seealso cref="System.String.Format(string, object, object, object)"/>): filename, indice del part, estensione</param>
		/// <returns>Un dizionario che ha come chiave il nome generato del part di destinazione, e come valore un array di stringhe contenente le righe del part</returns>
		public static Dictionary<string, string[]> SpezzaFile(string filePath, decimal numRecordPerFile, string fileNamePattern)
		{
			Dictionary<string, string[]> ret = new Dictionary<string, string[]>();
			string[] fileDaSpezzare = File.ReadAllLines(filePath);
			int len = (int)numRecordPerFile;
			bool EOF = false;
			for (int i = 0; !EOF; i++)
			{
				int offset = i * len;
				EOF = offset == fileDaSpezzare.Length;
				if (EOF) break;
				string[] lotto;
				string path = Path.GetDirectoryName(filePath);
				string filename = Path.GetFileNameWithoutExtension(filePath);
				string ext = Path.GetExtension(filePath);
				string filenameLotto = Path.Combine(path, String.Format(fileNamePattern, filename, i + 1, ext));
				if ((offset + len) > fileDaSpezzare.Length)
				{
					lotto = new string[fileDaSpezzare.Length - offset];
					Array.Copy(fileDaSpezzare, offset, lotto, 0, fileDaSpezzare.Length - offset);
					EOF = true;
				}
				else
				{
					lotto = new string[len];
					Array.Copy(fileDaSpezzare, offset, lotto, 0, len);
				}
				ret.Add(filenameLotto, lotto);
			}
			return ret;
		}

		/// <summary>
		/// Metodo di conversione di unità di dati.
		/// <example>
		/// 1 GB returns 1073741824<br/>
		/// 4 KB returns 4096
		/// </example>
		/// </summary>
		/// <param name="length">Unità di dati espressa in grandezza multipla.</param>
		/// <returns>Unità di dati espressa in grandezza minima (Byte).</returns>
		public static long ParseLength(string length)
		{
			double pow = 0;
			int multiplier = 0, baseLength = 1;
			Int32.TryParse(length.Remove(length.Length - 2), out baseLength);
			if (length.EndsWith(PBYTE, StringComparison.CurrentCultureIgnoreCase))
				multiplier = 5;
			else if (length.EndsWith(TBYTE, StringComparison.CurrentCultureIgnoreCase))
				multiplier = 4;
			else if (length.EndsWith(GBYTE, StringComparison.CurrentCultureIgnoreCase))
				multiplier = 3;
			else if (length.EndsWith(MBYTE, StringComparison.CurrentCultureIgnoreCase))
				multiplier = 2;
			else if (length.EndsWith(KBYTE, StringComparison.CurrentCultureIgnoreCase))
				multiplier = 1;
			else
				multiplier = 0;
			return baseLength * (long)Math.Pow(multiplier, pow);
		}

		public static bool IsNullOrEmpty(IEnumerable list)
		{
			return (list == null || (list != null ? !list.GetEnumerator().MoveNext() : true));
		}

		/// <summary>
		/// Converts the specified string representation of an enumeration to its generic given enumeration type.
		/// </summary>
		/// <typeparam name="T">Type pf enumeration</typeparam>
		/// <param name="en">This object</param>
		/// <param name="value">String representation of this object</param>
		/// <returns>Generic given type of enumeration</returns>
		public static T Parse<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}

		/// <summary>
		/// Converts the specified string representation of an enumeration to its generic given enumeration type.
		/// </summary>
		/// <typeparam name="T">Type pf enumeration</typeparam>
		/// <param name="en">This object</param>
		/// <param name="value">String representation of this object</param>
		/// <param name="result">Generic given type of enumeration</param>
		/// <returns>True if the conversion was made possible, false otherwise</returns>
		public static bool TryParse<T>(string value, out T result)
		{
			bool ret = true;
			result = default(T);
			try
			{
				result = Parse<T>(value);
			}
			catch { ret = false; }
			return ret;
		}

		public static bool IsNullOrEmpty<T>(T obj)
		{
			bool ret = false;
			if (obj != null)
			{
				Type t = typeof(T);
				int counter, members;
				object val = null;

				PropertyInfo[] properties = t.GetProperties();
				counter = 0;
				members = properties.Length;
				properties.ToList().ForEach(
					delegate(PropertyInfo pinfo)
					{
						val = pinfo.GetValue(obj, null);
						if (val == null || val == val.DefaultValue()) counter++;
					});

				if (members > counter)
				{
					FieldInfo[] fields = t.GetFields();
					counter = 0;
					members = fields.Length;
					fields.ToList().ForEach(
						delegate(FieldInfo finfo)
						{
							val = finfo.GetValue(obj);
							if (val == null || val == val.DefaultValue()) counter++;
						});

					if (members < counter) { ret = true; }
				}
				else { ret = true; }
			}
			else
			{
				ret = true;
			}
			return ret;
		}
	}
}
