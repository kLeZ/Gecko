using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Gecko.Parsing
{
	using GenericUtils;

	/// <summary>
	/// Descrive la modalità in cui viene fornito il contenuto del file.
	/// </summary>
	public enum ContentType
	{
		Path,
		StringContent,
		StreamContent,
		ByteArray
	}

	/// <summary>
	/// Si occupa di effettuare il parsing di un file di testo delimitato.
	/// </summary>
	public class TextFileParser
	{
		#region Proprietà
		private const string FMT = "Il campo {0} ha generato la seguente eccezione: \"{1}\"";

		private ContentType _ContentType = ContentType.Path;
		/// <summary>
		/// Contiene il valore del tipo di contenuto da cui ricavare i dati da elaborare.
		/// Per default il percorso del file.
		/// </summary>
		public ContentType ContentType
		{
			get { return _ContentType; }
			set { _ContentType = value; }
		}

		private string _FilePath;
		/// <summary>
		/// Il percorso del file di testo.
		/// </summary>
		public string FilePath
		{
			get { return _FilePath; }
			set { _FilePath = value; }
		}

		private string _FileContent;
		/// <summary>
		/// Il contenuto del file in formato stringa.
		/// </summary>
		public string StringContent
		{
			get { return _FileContent; }
			set { _FileContent = value; }
		}

		private FileStream _StreamContent;
		/// <summary>
		/// Il contenuto del file in formato FileStream.
		/// </summary>
		public FileStream StreamContent
		{
			get { return _StreamContent; }
			set { _StreamContent = value; }
		}

		private byte[] _FileBytes;
		/// <summary>
		/// Il contenuto del file in byte.
		/// </summary>
		public byte[] FileBytes
		{
			get { return _FileBytes; }
			set { _FileBytes = value; }
		}

		private char[] _ColumnSeparator = new char[] { '|', ';' };
		/// <summary>
		/// Separatore di colonna.
		/// Per default '|', ';'.
		/// </summary>
		public char[] ColumnSeparator
		{
			get { return _ColumnSeparator; }
			set { _ColumnSeparator = value; }
		}

		private char[] _RowSeparator = new char[] { '\r', '\n', '\0' };
		/// <summary>
		/// Separatore di riga.
		/// Per default '\r', '\n', '\0'.
		/// </summary>
		public char[] RowSeparator
		{
			get { return _RowSeparator; }
			set { _RowSeparator = value; }
		}

		private char[] _TrimCharacters = new char[] { ' ', '\t' };
		/// <summary>
		/// Caratteri di trim.
		/// Per default ' ', '\t'.
		/// </summary>
		public char[] TrimCharacters
		{
			get { return _TrimCharacters; }
			set { _TrimCharacters = value; }
		}

		private List<DataInformation> _FilePosition = new List<DataInformation>();
		/// <summary>
		/// Dizionario che indica la posizione di un dato e la relativa chiave nel file.
		/// </summary>
		public List<DataInformation> FilePosition
		{
			get { return _FilePosition; }
			set { _FilePosition = value; }
		}

		private StringSplitOptions _SplitOptions = StringSplitOptions.RemoveEmptyEntries;
		/// <summary>
		/// Opzioni di split per le righe del file.
		/// Per default esclude le righe vuote.
		/// </summary>
		public StringSplitOptions RowSplitOptions
		{
			get { return _SplitOptions; }
			set { _SplitOptions = value; }
		}

		private List<Log> logs = new List<Log>();
		/// <summary>
		/// Lista di log dell'elaborazione, vuota se tutto ok.
		/// </summary>
		public List<Log> Logs
		{
			get { return logs; }
			set { logs = value; }
		}

		private Encoding _Enc = Encoding.ASCII;
		/// <summary>
		/// Il tipo di codifica da adottare per la lettura del file
		/// </summary>
		public Encoding Encoding
		{
			get { return _Enc; }
			set { _Enc = value; }
		}

		private bool _ContinueOnError = true;
		/// <summary>
		/// Definisce la modalità di gestione degli errori, se true (default) continua e logga l'errore, altrimenti esce al primo errore
		/// </summary>
		public bool ContinueOnError
		{
			get { return _ContinueOnError; }
			set { _ContinueOnError = value; }
		}

		private Dictionary<ContentType, Action> ActionList;
		#endregion

		#region Costruttori
		/// <summary>
		/// Istanzia un oggetto di tipo TextFileParser.
		/// </summary>
		/// <param name="fct">Tipo di contenuto del file. A questo livello si accetta 'Path' o 'StringContent'</param>
		/// <param name="s">Il percorso del file su disco, oppure il contenuto del file.</param>
		/// <param name="FilePosition">Lista di <see cref="Gecko.Parsing.DataInformation"/></param>
		public TextFileParser(ContentType fct, string s, List<DataInformation> FilePosition)
		{
			InitActionList();
			this.FilePosition = FilePosition;
			this.ContentType = fct;
			switch (fct)
			{
				case ContentType.Path:
					{
						this.FilePath = s;
						break;
					}
				case ContentType.StringContent:
					{
						this.StringContent = s;
						break;
					}
			}
		}

		/// <summary>
		/// Istanzia un oggetto di tipo TextFileParser.
		/// </summary>
		/// <param name="fs">Un FileStream contenente informazioni sul file e il suo contenuto.</param>
		/// <param name="FilePosition">Lista di <see cref="Gecko.Parsing.DataInformation"/></param>
		public TextFileParser(FileStream fs, List<DataInformation> FilePosition)
		{
			InitActionList();
			this.FilePosition = FilePosition;
			this.ContentType = ContentType.StreamContent;
			this.StreamContent = fs;
		}

		/// <summary>
		/// Istanzia un oggetto di tipo TextFileParser.
		/// </summary>
		/// <param name="b">Il contenuto del file in byte.</param>
		/// <param name="FilePosition">Lista di <see cref="Gecko.Parsing.DataInformation"/></param>
		public TextFileParser(byte[] b, List<DataInformation> FilePosition)
		{
			InitActionList();
			this.FilePosition = FilePosition;
			this.ContentType = ContentType.ByteArray;
			this.FileBytes = b;
		}
		#endregion

		#region Metodi pubblici
		/// <summary>
		/// Estrae i dati da un file di testo a lunghezza variabile con delimitatore.
		///	
		/// E' possibile specificare i separatori di colonna, i separatori di riga, 
		/// le opzioni di separazione delle righe e i caratteri per i quali effettuare
		/// la pulizia dei dati ricavati.
		/// E' inoltre possibile specificare la posizione di ogni dato nel file tramite la proprietà FilePosition.
		/// </summary>
		/// <returns>Una lista di oggetti e un codice di ritorno, 0 se tutto ok.</returns>
		public List<T> ExtractData<T>(out int retCode)
		{
			string campo = "";
			int i = 0;
			retCode = 0;
			List<T> ret = new List<T>();
			try
			{
				ActionList[ContentType]();
				string[] Rows = this.StringContent.Split(RowSeparator, RowSplitOptions);
				retCode = Rows.Length;
				for (i = 0; i < Rows.Length; i++)
				{
					try
					{
						T obj = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(null);
						int cellsnumber = typeof(T).GetProperties().Length;
						string[] Cells = Rows[i].Split(ColumnSeparator, cellsnumber, StringSplitOptions.None);
						for (int j = 0; j < Cells.Length; j++)
						{
							campo = FilePosition[j].Key;
							string value = Cells[FilePosition[j].Position].Trim(TrimCharacters);
							obj = GenericsReflector.SetProperty<T>(obj, FilePosition[j].Key, value);
						}
						ret.Add(obj);
						retCode--;
					}
					catch (Exception ex)
					{
						if (!ContinueOnError) throw;
						else
						{
							Log log = new Log(i + 1, String.Format(FMT, campo, ex));
							logs.Add(log);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log log = new Log(i + 1, String.Format(FMT, campo, ex));
				logs.Add(log);
			}
			return ret;
		}

		/// <summary>
		/// Estrae il numero di righe nel file
		/// </summary>
		/// <returns>intero contenente il numero di righe</returns>
		public int GetCountRighe()
		{
			ActionList[ContentType]();
			string[] Rows = this.StringContent.Split(RowSeparator, RowSplitOptions);
			return Rows.Length;
		}
		#endregion

		#region InitActionList
		private void InitActionList()
		{
			ActionList = new Dictionary<ContentType, Action>();
			ActionList.Add(ContentType.StreamContent, new Action(LoadFileStream));
			ActionList.Add(ContentType.Path, new Action(LoadFilePath));
			ActionList.Add(ContentType.ByteArray, new Action(LoadBytes));
			ActionList.Add(ContentType.StringContent, new Action(LoadStringContent));
		}
		#endregion

		#region Load Methods
		private void LoadFileStream()
		{
			int len = (int)this.StreamContent.Length;
			byte[] b = new byte[len];

			if (len > 0)
				this.StreamContent.Read(b, 0, len);
			else
				throw new Exception("La lunghezza del file deve essere maggiore di zero!");

			this.FileBytes = b;
			this.StreamContent.Close();
			ActionList[ContentType.ByteArray]();
		}

		private void LoadBytes()
		{
			this.StringContent = Encoding.GetString(this.FileBytes);
			this.FileBytes = null;
			this.ContentType = ContentType.StringContent;
		}

		private void LoadStringContent() { }

		private void LoadFilePath()
		{
			this.StreamContent = File.OpenRead(this.FilePath);
			ActionList[ContentType.StreamContent]();
		}
		#endregion
	}
}
