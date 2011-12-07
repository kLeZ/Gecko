using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Gecko.IO
{
	public class ZipWork
	{
		/// <summary>
		/// Crea un file .zip dei file della directory passata
		/// </summary>
		/// <param name="FolderToWriteZip">Cartella dentro la quale scrivere il file zip</param>
		/// <param name="FolderToZip">Directory contente i file da zippare</param>
		/// <param name="zippedFileName">Percorso e nome del file .zip</param>
		/// <param name="CompressionLevel">Livello di compressione: 0 (no compressione) - 9 (massima compressione)</param>
		/// <param name="recurse">Se true include le sottodirectory</param>
		/// <returns>il percorso completo del file zip creato</returns>
		public string ZipFolder(string FolderToWriteZip, string FolderToZip, string zippedFileName, int CompressionLevel, bool recurse)
		{
			string zipFile = Path.Combine(FolderToWriteZip, zippedFileName);
			ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFile));
			zipStream.SetLevel(CompressionLevel);
			Create(zipStream, FolderToZip, FolderToZip, recurse);
			zipStream.Finish();
			zipStream.Close();
			return zipFile;
		}

		/// <summary>
		/// metodo per verificare 
		/// se un file .zip sia corrotto
		/// </summary>
		/// <param name="stZip"><see cref="System.IO.Stream"/> contenente il file .zip</param>
		/// <param name="Dimension">dimensione del file</param>
		/// <returns>
		/// true file zip valido
		/// false file zip corrotto</returns>
		public bool CheckZipFile(Stream stZip, long Dimension)
		{
			bool bResult = true;
			byte[] streamFileZip = new byte[Dimension];
			stZip.Read(streamFileZip, 0, streamFileZip.Length);
			ZipInputStream zipIn = new ZipInputStream(stZip);

			if (streamFileZip.Length == 0)
				bResult = false;
			return bResult;
		}

		/// <summary>
		/// Decomprime un file compresso in input, spostandolo nella directory specificata in input
		/// </summary>
		/// <param name="FolderToUnZip">Directory di destinazione del file</param>
		/// <param name="zippedFileName">Nome del file compresso</param>
		public void UnZipFile(string FolderToUnZip, string zippedFileName)
		{
			ZipInputStream zipIStream = new ZipInputStream(File.OpenRead(zippedFileName));
			ZipEntry theEntry;

			try
			{
				while ((theEntry = zipIStream.GetNextEntry()) != null)
				{
					string destinationDirectory = FolderToUnZip;
					if (theEntry.IsDirectory)
						Directory.CreateDirectory(Path.Combine(destinationDirectory, theEntry.Name));
					else
					{
						int size = 2048;
						byte[] data = new byte[size];

						string nomeFile = Path.GetFileName(theEntry.Name);
						FileStream fs = new FileStream(Path.Combine(destinationDirectory, nomeFile), FileMode.Create);

						try
						{
							while ((size = zipIStream.Read(data, 0, data.Length)) > 0)
							{
								fs.Write(data, 0, size);
							}
						}
						catch (Exception ex)
						{
							throw ex;
						}
						finally
						{
							fs.Flush();
							fs.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				zipIStream.Close();
			}
		}

		#region Metodi Privati
		private void Create(ZipOutputStream ZipFile, string RootFolder, string FolderToZip, bool recurse)
		{
			if (recurse)
			{
				string[] SubFolders = Directory.GetDirectories(FolderToZip);
				foreach (string Folder in SubFolders)
				{
					Create(ZipFile, RootFolder, Folder, recurse);
				}
			}

			string RelativePathFolder = FolderToZip.Remove(0, RootFolder.Length);
			RelativePathFolder = (RelativePathFolder.StartsWith(@"\") ? RelativePathFolder.Remove(0, 1) : RelativePathFolder);

			if (!String.IsNullOrEmpty(RelativePathFolder))
			{
				RelativePathFolder = String.Concat(RelativePathFolder, "/");
				ZipEntry entry;
				entry = new ZipEntry(RelativePathFolder);
				entry.DateTime = DateTime.Now;
				ZipFile.PutNextEntry(entry);
			}

			string[] files = Directory.GetFiles(FolderToZip);
			foreach (string file in files)
			{
				AddZipEntry(ZipFile, RelativePathFolder, file);
			}
		}

		private void AddZipEntry(ZipOutputStream ZipStream, string CurrentFolder, string FileName)
		{
			Directory.SetCurrentDirectory(Path.GetDirectoryName(FileName));
			FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			ZipEntry entry = new ZipEntry(String.Concat(CurrentFolder, Path.GetFileName(FileName)));

			entry.DateTime = File.GetLastWriteTime(FileName);
			byte[] buffer = new byte[fs.Length];
			fs.Read(buffer, 0, buffer.Length);
			entry.Size = fs.Length;
			fs.Close();

			ZipStream.PutNextEntry(entry);
			ZipStream.Write(buffer, 0, buffer.Length);
		}
		#endregion Metodi Privati
	}
}
