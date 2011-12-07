
namespace Gecko.Parsing
{
	/// <summary>
	/// Classe che contiene informazioni su un particolare dato (coppia chiave-valore)
	/// </summary>
	public class DataInformation
	{
		private int _Pos;
		/// <summary>
		/// Posizione del dato all'interno di una struttura dati (in base zero)
		/// </summary>
		public int Position
		{
			get { return _Pos; }
			set { _Pos = value; }
		}

		private string _Key;
		/// <summary>
		/// Chiave del dato
		/// </summary>
		public string Key
		{
			get { return _Key; }
			set { _Key = value; }
		}
	}
}
