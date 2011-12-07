using System;
using System.Collections.Generic;

namespace Gecko.Net
{
	using System.IO;
	using Log;

	public class FTPFileTransferer
	{
		private string Host, path, UserName, Password, PathOrigine;
		private bool BinaryMode;
		private int Port, Log;
		private Logger MyLog;

		/// <summary>
		/// Costruttore. La classe si occupa di trasferire un file via FTP.
		/// </summary>
		/// <param name="ftpparms">I parametri di connessione al sito FTP.</param>
		/// <param name="PathOrigine">Il percorso di origine del file di cui effettuare l'upload o dove scrivere il file di cui effettuare il download.</param>
		/// <param name="log">Intero che indica le possibilità di logging, attualmente sono supportati i valori <br/>
		/// 1: Log su StdOut
		/// 2: Log su File
		/// 3: Log su entrambi i precedenti</param>
		/// <param name="logger">Classe logger usata per effettuare il log degli eventi.</param>
		public FTPFileTransferer(ParametriFTP ftpparms, string PathOrigine, int log, Logger logger)
		{
			this.Host = ftpparms.Host;
			this.path = ftpparms.Path;
			this.UserName = ftpparms.UserName;
			this.Password = ftpparms.Password;
			this.BinaryMode = ftpparms.BinaryMode;
			this.Port = ftpparms.Port;
			this.PathOrigine = PathOrigine;
			this.Log = log;
			MyLog = logger;
			MyLog.OnNewLineLogged += new NewLineLogged(MyLog_OnNewLineLogged);
		}

		#region Logging
		private void WriteLog(String msg)
		{
			MyLog.Log(msg);
		}

		private void WriteLog(String msg, string livello)
		{
			MyLog.Log(msg, livello);
		}

		private void MyLog_OnNewLineLogged(object sender, NewLineLoggedEventArgs e)
		{
			switch (Log)
			{
				case 1:
					{
						Console.WriteLine("{0}{1}", e.LogLine, Environment.NewLine);
						break;
					}
				case 2:
					{
						MyLog.WriteLog(e.LogLine, e.LogLevel);
						break;
					}
				case 3:
					{
						Console.WriteLine("{0}{1}", e.LogLine, Environment.NewLine);
						break;
					}
				default:
					{
						MyLog.WriteLog(e.LogLine, e.LogLevel);
						break;
					}
			}
		}
		#endregion

		public bool Download(string FileName, bool debug)
		{
			bool ret = false;
			FTPFactory ff = new FTPFactory();

			try
			{
				WriteLog("Starting...");

				ff.setDebug(debug);
				ff.setRemoteHost(Host);
				ff.setRemotePort(Port);
				ff.setRemoteUser(UserName);
				ff.setRemotePass(Password);
				ff.login();

				WriteLog(String.Concat("Connected to ", Host));

				ff.chdir(path);

				if (!path.Equals("."))
					WriteLog(String.Concat("Current directory is ", path));

				ff.setBinaryMode(BinaryMode);

				DateTime InizioTrasferimento;
				DateTime FineTrasferimento;
				bool OK = false;
				string exMsg = "";
				FileInfo file = new FileInfo(Path.Combine(PathOrigine, FileName));
				long localFileSize = 0;
				long remoteFileSize = 0;
				localFileSize = file.Length;
				InizioTrasferimento = DateTime.Now;

				try
				{
					ff.download(FileName, Path.Combine(PathOrigine, FileName));
					remoteFileSize = ff.getFileSize(FileName);
					OK = true;
				}
				catch (Exception ex)
				{
					exMsg = ex.Message;
				}
				finally
				{
					FineTrasferimento = DateTime.Now;
					string fmt = "==>\t{0}\t-->\t{1}\t{2}\t{3}\t{4}\t{5}{6}";
					List<String> parametri = new List<string>();
					parametri.Add(FileName);
					parametri.Add(InizioTrasferimento.ToLocalTime().ToString());
					parametri.Add(FineTrasferimento.ToLocalTime().ToString());
					parametri.Add((FineTrasferimento - InizioTrasferimento).ToString());
					parametri.Add(localFileSize.ToString());
					parametri.Add(remoteFileSize.ToString());
					if (OK) parametri.Add(String.Concat("\t", "OK"));
					else parametri.Add(String.Concat("\n\t\t", exMsg));

					WriteLog(String.Format(fmt, parametri.ToArray()));
				}

				ff.close();
				WriteLog("Trasferimento completato con successo");
				ret = true;
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString(), "E");
				ret = false;
			}
			return ret;
		}

		public bool Upload(string FileName, bool debug)
		{
			bool ret = false;
			FTPFactory ff = new FTPFactory();

			try
			{
				WriteLog("Starting...");

				ff.setDebug(debug);
				ff.setRemoteHost(Host);
				ff.setRemotePort(Port);
				ff.setRemoteUser(UserName);
				ff.setRemotePass(Password);
				ff.login();

				WriteLog(String.Concat("Connected to ", Host));

				ff.chdir(path);

				if (!path.Equals("."))
					WriteLog(String.Concat("Current directory is ", path));

				ff.setBinaryMode(BinaryMode);

				DateTime InizioTrasferimento;
				DateTime FineTrasferimento;
				bool OK = false;
				string exMsg = "";
				FileInfo file = new FileInfo(Path.Combine(PathOrigine, FileName));
				long localFileSize = 0;
				long remoteFileSize = 0;
				localFileSize = file.Length;
				InizioTrasferimento = DateTime.Now;

				try
				{
					ff.upload(Path.Combine(PathOrigine, FileName));
					remoteFileSize = ff.getFileSize(FileName);
					OK = true;
				}
				catch (Exception ex)
				{
					exMsg = ex.Message;
				}
				finally
				{
					FineTrasferimento = DateTime.Now;
					string fmt = "==>\t{0}\t-->\t{1}\t{2}\t{3}\t{4}\t{5}{6}";
					List<String> parametri = new List<string>();
					parametri.Add(FileName);
					parametri.Add(InizioTrasferimento.ToLocalTime().ToString());
					parametri.Add(FineTrasferimento.ToLocalTime().ToString());
					parametri.Add((FineTrasferimento - InizioTrasferimento).ToString());
					parametri.Add(localFileSize.ToString());
					parametri.Add(remoteFileSize.ToString());
					if (OK) parametri.Add(String.Concat("\t", "OK"));
					else parametri.Add(String.Concat("\n\t\t", exMsg));

					WriteLog(String.Format(fmt, parametri.ToArray()));
				}

				ff.close();
				WriteLog("Trasferimento completato con successo");
				ret = true;
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString(), "E");
				ret = false;
			}
			return ret;
		}
	}
}
