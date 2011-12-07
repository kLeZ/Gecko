using System;
using System.IO;

namespace Gecko.Log
{
	public delegate void NewLineLogged(object sender, NewLineLoggedEventArgs e);

	/// <summary>
	/// Classe con metodi statici per creare un file Es. log.txt
	/// dove vengono tracciate le azioni e gli
	/// eventuali errori del programma
	/// </summary>
	public class Logger
	{
		private string _LogPath;
		/// <summary>
		/// il path dove verrà creato il file di log
		/// </summary>
		public string LogPath
		{
			get { return _LogPath; }
			private set { _LogPath = value; }
		}

		private string _FileName;
		/// <summary>
		/// il nome del file di log Es. "log.txt"
		/// </summary>
		public string FileName
		{
			get { return _FileName; }
			private set { _FileName = value; }
		}

		private string _LineFormat = "{0} [{1}] ----- {2}";
		public string LineFormat
		{
			get { return _LineFormat; }
			set { _LineFormat = value; }
		}

		private string _DateFormat = "yyyy/MM/dd HH:mm:ss";
		public string DateFormat
		{
			get { return _DateFormat; }
			set { _DateFormat = value; }
		}

		public event NewLineLogged OnNewLineLogged;

		/// <summary>
		/// metodo che veririfica che il la directory e il file di log.txt
		/// esiste e se non non esiste li crea 
		/// </summary>
		/// <returns>variabile boolena
		/// 1-TRUE il path ed il file di log esiste o la creazione è andata a buon file
		/// 2-FALSE creazione del file non è andata a buon file e il file di log non esiste</returns>
		private bool CheckLogFile()
		{
			bool ret = true;
			if (!Directory.Exists(LogPath))
			{
				try
				{
					Directory.CreateDirectory(LogPath);
					ret = true;
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		/// <summary>
		/// metodo che crea il file di log.txt
		/// </summary>
		private void CreateLogFile()
		{
			if (CheckLogFile() && !File.Exists(Path.Combine(LogPath, FileName)))
			{
				StreamWriter fsW = null;
				try
				{
					fsW = File.CreateText(Path.Combine(LogPath, FileName));
				}
				catch (Exception)
				{
					throw new Exception("Errore nella creazione della directory del file di log o del file di log stesso");
				}
				finally
				{
					fsW.Close();
				}
			}
		}

		/// <summary>
		/// metodo che compone il messaggio da scrivere nel file di log
		/// </summary>
		/// <param name="sMessage">testo del messaggio</param>
		/// <param name="sTypeMsg">tipo di messaggio
		/// 1 - I -- Information
		/// 2 - E -- Error 
		/// 3 - W -- Warning
		/// 4 - D -- Debug</param>
		public void WriteLog(string sMessage, string sTypeMsg)
		{
			CheckLogFile();
			string date = DateTime.Now.ToString(DateFormat);
			string msg = String.Format(LineFormat, date, sTypeMsg.ToUpper(), sMessage);
			WriteIntoLogFile(msg);
		}

		/// <summary>
		/// metodo che scrive i messaggi all'interno del file di log
		/// </summary>
		/// <param name="sMessage">messaggio da scrivere</param>
		private void WriteIntoLogFile(string sMessage)
		{
			StreamWriter strW = File.AppendText(Path.Combine(LogPath, FileName));
			strW.Write(sMessage);
			strW.Write(strW.NewLine);
			strW.Close();
		}

		public void Log(string text)
		{
			OnNewLineLogged.Invoke(this, new NewLineLoggedEventArgs(text, "I"));
		}
		public void Log(string text, string loglevel)
		{
			OnNewLineLogged.Invoke(this, new NewLineLoggedEventArgs(text, loglevel));
		}
	}

	public class NewLineLoggedEventArgs : EventArgs
	{
		private string _LogLine = null, _Level = null;

		public NewLineLoggedEventArgs(string LogLine, string level)
		{
			_LogLine = LogLine;
			_Level = level;
		}

		public string LogLine { get { return _LogLine; } }
		public string LogLevel { get { return _Level; } }
	}
}
