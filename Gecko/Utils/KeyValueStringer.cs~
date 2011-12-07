using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Gecko.Utils
{
	/// <summary>
	/// This class acts as a converter between a String key-value pair collection and a Dictionary collection.<br />
	/// Its main goal is to manage a key value string as a web querystring could be,<br />
	/// in a general way without mistakes for the developer.
	/// </summary>
	public class KeyValueStringer
	{
		/// <summary>
		/// A constant value for the Field Separator (that one which divides the key from the value) its value is ":"
		/// </summary>
		public const string FIELD_SEPARATOR = ":";

		/// <summary>
		/// A constant value for the Pair Separator (that one which divides a key value pair from another) its value is ";"
		/// </summary>
		public const string PAIR_SEPARATOR = ";";

		private string fieldSeparator = FIELD_SEPARATOR;
		/// <summary>
		/// A variable value for the Field Separator (that one which divides the key from the value)<br />
		/// by default is FIELD_SEPARATOR
		/// </summary>
		public string FieldSeparator
		{
			get { return fieldSeparator; }
			set { fieldSeparator = value; }
		}

		private string pairSeparator = PAIR_SEPARATOR;
		/// <summary>
		/// A variable value for the Pair Separator (that one which divides a key value pair from another)<br />
		/// by default is PAIR_SEPARATOR
		/// </summary>
		public string PairSeparator
		{
			get { return pairSeparator; }
			set { pairSeparator = value; }
		}

		private StringSplitOptions splitOption = StringSplitOptions.RemoveEmptyEntries;
		/// <summary>
		/// An option to manage the split function for the string value <br />
		/// See <see cref="System.StringSplitOption"/> for more details.
		/// </summary>
		public StringSplitOptions SplitOption
		{
			get { return splitOption; }
			set { splitOption = value; }
		}

		private Dictionary<string, string> dictVal;
		/// <summary>
		/// A dictionary that will contain the key value pairs
		/// </summary>
		public Dictionary<string, string> DictValue
		{
			get { return dictVal; }
		}

		private string strVal;
		/// <summary>
		/// A string value that contains all the key-value pairs separated by the field and pair separators
		/// </summary>
		public string StrValue
		{
			get { return strVal; }
		}

		/// <summary>
		/// This constructor sets the string value and performs the conversion directly to dictionary.<br />
		/// The converted value will be accessible from the DictionaryValue property of this instance.
		/// </summary>
		/// <param name="strValue">A string value that contains all the key-value pairs separated by the field and pair separators</param>
		public KeyValueStringer(string strValue)
		{
			Set(strValue);
		}

		/// <summary>
		/// This constructor sets the string value and performs the conversion directly to dictionary.<br />
		/// The converted value will be accessible from the DictionaryValue property of this instance.
		/// </summary>
		/// <param name="strValue">A string value that contains all the key-value pairs separated by the field and pair separators</param>
		/// <param name="splitOption"> option to manage the split function for the string value <br />
		/// See <seealso cref="System.StringSplitOption"/> for more details.</param>
		public KeyValueStringer(string strValue, StringSplitOptions splitOption)
		{
			this.SplitOption = splitOption;
			Set(strValue);
		}

		/// <summary>
		/// This constructor sets the dictionary value and performs the conversion directly to string.<br />
		/// The converted value will be accessible from the StringValue property of this instance.
		/// </summary>
		/// <param name="dictValue">A dictionary that will contain the key value pairs</param>
		public KeyValueStringer(Dictionary<string, string> dictValue)
		{
			Set(dictValue);
		}

		/// <summary>
		/// This method sets the string value and performs the conversion directly to dictionary.<br />
		/// The converted value will be accessible from the DictionaryValue property of this instance.
		/// </summary>
		/// <param name="strValue">A string value that contains all the key-value pairs separated by the field and pair separators</param>
		public void Set(string strValue)
		{
			this.strVal = strValue;
			this.dictVal = StringToDictionary(strValue, SplitOption, FieldSeparator, PairSeparator);
		}

		/// <summary>
		/// This method sets the dictionary value and performs the conversion directly to string.<br />
		/// The converted value will be accessible from the StringValue property of this instance.
		/// </summary>
		/// <param name="dictValue">A dictionary that will contain the key value pairs</param>
		public void Set(Dictionary<string, string> dictValue)
		{
			this.dictVal = dictValue;
			this.strVal = DictionaryToString(dictValue, FieldSeparator, PairSeparator);
		}

		/// <summary>
		/// This static method performs a conversion from a dictionary collection of values to a formatted string which will be built using the given separators
		/// </summary>
		/// <param name="dictValue">A dictionary that will contain the key value pairs</param>
		/// <param name="fs">A variable value for the Field Separator (that one which divides the key from the value)<br />
		/// by default is FIELD_SEPARATOR</param>
		/// <param name="ps">A variable value for the Pair Separator (that one which divides a key value pair from another)<br />
		/// by default is PAIR_SEPARATOR</param>
		/// <returns>A formatted string which will be built using the given separators</returns>
		public static string DictionaryToString(Dictionary<string, string> dictValue, string fs, string ps)
		{
			StringBuilder ret = new StringBuilder();
			foreach (KeyValuePair<string, string> keyVal in dictValue)
				ret.Append(keyVal.Key).Append(String.IsNullOrEmpty(fs) ? FIELD_SEPARATOR : fs).Append(keyVal.Value).Append(String.IsNullOrEmpty(ps) ? PAIR_SEPARATOR : ps);
			return ret.ToString();
		}

		/// <summary>
		/// This static method performs a conversion from a string value to a dictionary collection of key value pairs detected into this string using the given separators
		/// </summary>
		/// <param name="strValue">formatted string of key value pairs</param>
		/// <param name="SplitOption">Option to pass to the String.Split function.<br />
		/// See <see cref="System.String.Split(char[], System.StringSplitOption)"/> for more details.</param>
		/// <param name="fs">A variable value for the Field Separator (that one which divides the key from the value)<br />
		/// by default is FIELD_SEPARATOR</param>
		/// <param name="ps"> variable value for the Pair Separator (that one which divides a key value pair from another)<br />
		/// by default is PAIR_SEPARATOR</param>
		/// <returns></returns>
		public static Dictionary<string, string> StringToDictionary(string strValue, StringSplitOptions SplitOption, string fs, string ps)
		{
			string pairsep = String.IsNullOrEmpty(ps) ? PAIR_SEPARATOR : ps;
			string fieldsep = String.IsNullOrEmpty(fs) ? FIELD_SEPARATOR : fs;
			Dictionary<string, string> ret = new Dictionary<string, string>();
			List<string> pairs = strValue.Split(pairsep.ToArray(), SplitOption).ToList();
			List<string> splPair;
			foreach (string pair in pairs)
			{
				splPair = pair.Split(fieldsep.ToArray(), SplitOption).ToList();
				ret.Add(splPair.FirstOrDefault(), splPair.LastOrDefault());
			}
			return ret;
		}
	}
}
