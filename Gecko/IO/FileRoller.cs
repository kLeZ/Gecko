using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gecko.IO
{
	using Utils;

	public class FileRoller
	{
		private DirectoryInfo pathToWrite;
		private StreamWriter writer;
		private string path;
		private DateTime fileDate;
		private int counter;

		public string FullName
		{
			get
			{
				string path = "", name = "";
				if (pathToWrite != null) path = pathToWrite.FullName;
				else path = Environment.CurrentDirectory;
				if (!String.IsNullOrEmpty(NamePattern)) name = NamePattern;
				else name = "FileRoller.txt";
				return Path.Combine(path, name);
			}
		}

		private string _FileName = "";
		/// <summary>
		/// Nome del file da rollare
		/// </summary>
		public string NamePattern
		{
			get { return _FileName; }
			set { _FileName = value; }
		}

		private string _MaxLength = "1 MB";
		/// <summary>
		/// Lunghezza massima dei dati espressa in grandezze multiple di Byte.<br/>
		/// <example>1TB, 1GB, 1MB, 1KB, 1B</example>
		/// </summary>
		public string MaxLength
		{
			get { return _MaxLength; }
			set { _MaxLength = value; }
		}

		/// <summary>
		/// Si occupa di gestire il rolling del file
		/// </summary>
		/// <param name="pathToWrite">Percorso dove andare a scrivere il file</param>
		/// <param name="FileName">Pattern di nome del file da scrivere</param>
		public FileRoller(DirectoryInfo pathToWrite, string FileName)
		{
			FileRollerBase(pathToWrite, FileName);
			Init();
		}

		/// <summary>
		/// Si occupa di gestire il rolling del file
		/// </summary>
		/// <param name="pathToWrite">Percorso dove andare a scrivere il file</param>
		/// <param name="MaxLength">Lunghezza massima del file superata la quale il file viene chiuso e ne viene aperto un altro</param>
		/// <param name="FileName">Pattern di nome del file da scrivere</param>
		public FileRoller(DirectoryInfo pathToWrite, string FileName, string MaxLength)
		{
			FileRollerBase(pathToWrite, FileName);
			this.MaxLength = MaxLength;
			Init();
		}

		/// <summary>
		/// Metodo interno per l'inizializzazione generica dei parametri
		/// </summary>
		/// <param name="pathToWrite">Percorso dove andare a scrivere il file</param>
		/// <param name="FileName">Pattern di nome del file da scrivere</param>
		private void FileRollerBase(DirectoryInfo pathToWrite, string FileName)
		{
			this.pathToWrite = pathToWrite;
			this.NamePattern = FileName;
			if (!pathToWrite.Exists)
				pathToWrite.Create();
			counter = Last();
		}

		/// <summary>
		/// Trova l'ultimo indice nei file scritti (Es.: Con la serie FileName.log.1, FileName.log.2, FileName.log.3, il valore di ritorno è 3)
		/// </summary>
		/// <returns>L'ultimo indice tra i file scritti</returns>
		private int Last()
		{
			FileInfo[] files = pathToWrite.GetFiles(String.Concat(NamePattern, ".*"));
			List<string> exts = new List<string>();
			foreach (FileInfo file in files)
				if (file.Name.StartsWith(NamePattern))
					exts.Add(Path.GetExtension(file.Name));
			List<int> iexts = exts.ConvertAll<int>(StringToInt);
			iexts.Sort();
			return iexts.LastOrDefault();
		}

		/// <summary>
		/// Coverte una stringa in intero
		/// </summary>
		/// <param name="s">Numero espresso come stringa</param>
		/// <returns>La stringa passata convertita in un numero</returns>
		private int StringToInt(string s)
		{
			int i = 0;
			Int32.TryParse(s, out i);
			return i;
		}

		/// <summary>
		/// Chiude il file e rilascia tutte le risorse associate
		/// </summary>
		public void Close()
		{
			if (writer != null)
			{
				try { writer.Flush(); writer.Close(); }
				catch { }
				writer.Dispose();
			}
		}

		/// <summary>
		/// Inizializza un file sulla base dei parametri impostati
		/// </summary>
		private void Init()
		{
			writer = File.CreateText(FullName);
			writer.AutoFlush = true;
		}

		/// <summary>
		/// Cambia il file corrente con uno nuovo, chiudendo il primo
		/// </summary>
		private void SwitchFile()
		{
			Close();
			string newPathfilename = String.Format("{0}.{1}", FullName, ++counter);
			File.Move(path, newPathfilename);
			Init();
		}

		/// <summary>
		/// Appende un messaggio al file
		/// </summary>
		/// <param name="msg">Il messaggio da scrivere</param>
		public void Append(string msg)
		{
			if (NeedToChangeLogFile()) SwitchFile();
			writer.Write(msg);
		}

		/// <summary>
		/// Appende un carattere di fine riga al file
		/// </summary>
		public void AppendLine()
		{
			AppendLine(String.Empty);
		}

		/// <summary>
		/// Appende un messaggio e un carattere di fine riga al file
		/// </summary>
		/// <param name="msg">Il messaggio da scrivere</param>
		public void AppendLine(string msg)
		{
			if (NeedToChangeLogFile()) SwitchFile();
			writer.WriteLine(msg);
		}

		/// <summary>
		/// Metodo che controlla la necessità di cambiare il file su cui scrivere (Roll), controllando che la lunghezza massima del file sia stata superata e che l'ultimo messaggio sia stato scritto per intero.
		/// </summary>
		/// <returns></returns>
		private bool NeedToChangeLogFile()
		{
			FileInfo fileInfos = new FileInfo(path);
			bool upraiseLength, flushLastMessage;
			upraiseLength = fileInfos.Length > Utilities.ParseLength(MaxLength);

			// L'aggiunta di flushLastMessage serve a evitare che i log siano frammentati, controllando che sia scritto il carattere di fine riga e ritorno a capo
			Close();
			flushLastMessage = CheckEndOfLastLine(fileInfos);
			if (!flushLastMessage || !upraiseLength)
			{
				writer = null;
				writer = File.AppendText(path);
				writer.AutoFlush = true;
			}

			return upraiseLength && flushLastMessage;
		}

		/// <summary>
		/// Controlla se l'ultima riga del file finisce con un carattere di nuova linea (<see cref="System.Environment.NewLine"/>)
		/// </summary>
		/// <param name="fileInfos"><see cref="System.IO.FileInfo"/></param>
		/// <returns>True se l'ultima riga del file finisce con un carattere di nuova linea, altrimenti False.</returns>
		private bool CheckEndOfLastLine(FileInfo fileInfos)
		{
			StreamReader reader = fileInfos.OpenText();
			bool flushLastMessage = reader.ReadToEnd().EndsWith(Environment.NewLine);
			reader.Close();
			reader.Dispose();
			reader = null;
			return flushLastMessage;
		}
	}
}
