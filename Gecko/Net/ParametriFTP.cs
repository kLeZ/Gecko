
namespace Gecko.Net
{
	public class ParametriFTP
	{
		private string _Host, _path, _UserName, _Password;
		private bool _BinaryMode;
		private int _Port;

		public string Host
		{
			get { return _Host; }
			set { _Host = value; }
		}

		public int Port
		{
			get { return _Port; }
			set { _Port = value; }
		}

		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		public string UserName
		{
			get { return _UserName; }
			set { _UserName = value; }
		}

		public string Password
		{
			get { return _Password; }
			set { _Password = value; }
		}

		public bool BinaryMode
		{
			get { return _BinaryMode; }
			set { _BinaryMode = value; }
		}
	}
}
