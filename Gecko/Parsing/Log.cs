using System;
using System.Collections.Generic;

namespace Gecko.Parsing
{
	public enum LogLevel
	{
		Information,
		Warning,
		Error
	}

	public class Log
	{
		private int? _ID;
		public int? ID
		{
			get { return _ID; }
			set { _ID = value; }
		}

		private LogLevel _LogCode;
		public LogLevel LogCode
		{
			get { return _LogCode; }
			set { _LogCode = value; }
		}

		private string _Descrizione;
		public string Descrizione
		{
			get { return _Descrizione; }
			set { _Descrizione = value; }
		}

		private DateTime _Data;
		public DateTime Data
		{
			get { return _Data; }
			set { _Data = value; }
		}

		public Log()
		{ }

		public Log(int? ID, LogLevel LogCode, string Descrizione, DateTime Data)
		{
			this.ID = ID;
			this.LogCode = LogCode;
			this.Descrizione = Descrizione;
			this.Data = Data;
		}

		public Log(int? ID, string Descrizione)
		{
			this.ID = ID;
			this.LogCode = LogLevel.Warning;
			this.Descrizione = Descrizione;
			this.Data = DateTime.Now;
		}

		public static LogLevel EvalLogLevel(string code)
		{
			if (code.Equals("I"))
			{
				return LogLevel.Information;
			}
			else if (code.Equals("W"))
			{
				return LogLevel.Warning;
			}
			else if (code.Equals("E"))
			{
				return LogLevel.Error;
			}
			else
				throw new FormatException("Wrong Log Code provided.");
		}

		public static string EvalLogLevel(LogLevel code, bool extended)
		{
			switch (code)
			{
				case LogLevel.Information:
					{
						if (extended)
							return "Informazione";
						else
							return "I";
					}
				case LogLevel.Warning:
					{
						if (extended)
							return "Avviso";
						else
							return "W";
					}
				case LogLevel.Error:
					{
						if (extended)
							return "Errore";
						else
							return "E";
					}
				default:
					{
						if (extended)
							return "Informazione";
						else
							return "I";
					}
			}
		}

		public override string ToString()
		{
			string fmt = "ID: {0}, Date: {1} LogLevel: {2}; '{3}'";
			List<string> parameters = new List<string>();
			parameters.Add(ID.ToString());
			parameters.Add(Data.ToString("dd/MM/yyyy hh:mm:ss.fff"));
			parameters.Add(EvalLogLevel(LogCode, true));
			parameters.Add(Descrizione);
			return String.Format(fmt, parameters.ToArray());
		}
	}
}
