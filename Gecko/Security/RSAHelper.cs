using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gecko.Security
{
	public class RSAHelper
	{
		private int strength = 4096;
		public string PathToKeysXml = "";
		public string publicKey = "";
		private string privateKey = "";
		private RSACryptoServiceProvider rsa;

		public RSAHelper(int strength, bool generateKey, string PathToKeysXml)
		{
			this.strength = strength;
			this.PathToKeysXml = PathToKeysXml;
			AssignParameter();
			if (generateKey)
			{
				AssignNewKey();
			}
		}

		private void AssignParameter()
		{
			const int PROVIDER_RSA_FULL = 1;
			const string CONTAINER_NAME = "GeckoContainer";
			CspParameters cspParams;
			cspParams = new CspParameters(PROVIDER_RSA_FULL);
			cspParams.KeyContainerName = CONTAINER_NAME;
			cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
			cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
			rsa = new RSACryptoServiceProvider(strength, cspParams);
		}

		#region NewKey
		private void AssignNewKey()
		{
			AssignParameter();

			//provide public and private RSA params
			StreamWriter writer = new StreamWriter(Path.Combine(PathToKeysXml, "privatekey.xml"));
			string publicPrivateKeyXML = rsa.ToXmlString(true);
			writer.Write(publicPrivateKeyXML);
			writer.Close();

			//provide public only RSA params
			writer = new StreamWriter(Path.Combine(PathToKeysXml, "privatekey.xml"));
			string publicOnlyKeyXML = rsa.ToXmlString(false);
			writer.Write(publicOnlyKeyXML);
			writer.Close();
		}
		#endregion

		public string EncryptData(string data2Encrypt)
		{
			AssignParameter();
			rsa.FromXmlString(publicKey);
			//read plaintext, encrypt it to ciphertext

			byte[] plainbytes = Encoding.UTF8.GetBytes(data2Encrypt);
			byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
			return Convert.ToBase64String(cipherbytes);
		}

		public string DecryptData(string data2Decrypt)
		{
			AssignParameter();

			byte[] getpassword = Convert.FromBase64String(data2Decrypt);
			rsa.FromXmlString(privateKey);

			//read ciphertext, decrypt it to plaintext
			byte[] plain = rsa.Decrypt(getpassword, false);
			return Encoding.UTF8.GetString(plain);
		}
	}
}
