using System.Security.Cryptography;
using System.Text;

namespace Gecko.Security
{
	public enum HashType
	{
		MD5,
		SHA1,
		SHA256,
		SHA384,
		SHA512
	}

	public class MD5SHAHelper
	{
		public static string Encrypt(string toEncrypt, HashType hashType, Encoding enc)
		{
			string ret = "";
			byte[] rawBytes = enc.GetBytes(toEncrypt);
			HashAlgorithm hash = GetHashProvider(hashType);
			byte[] result = hash.ComputeHash(rawBytes);
			ret = enc.GetString(result);
			return ret;
		}

		public static HashAlgorithm GetHashProvider(HashType type)
		{
			HashAlgorithm hash = null;
			switch (type)
			{
				case HashType.MD5:
					{
						hash = new MD5CryptoServiceProvider();
						break;
					}
				case HashType.SHA1:
					{
						hash = new SHA1CryptoServiceProvider();
						break;
					}
				case HashType.SHA256:
					{
						hash = new SHA256CryptoServiceProvider();
						break;
					}
				case HashType.SHA384:
					{
						hash = new SHA384CryptoServiceProvider();
						break;
					}
				case HashType.SHA512:
					{
						hash = new SHA512CryptoServiceProvider();
						break;
					}
			}
			return hash;
		}
	}
}
