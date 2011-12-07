using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Windows.Forms;
using Gecko.Collections;
using Gecko.Net;
using Gecko.Parsing;
using Gecko.Randomics;
using Gecko.Extensions.CollectionExtensions;
using System.Linq;

namespace TestGecko
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			MersenneTwister mt = new MersenneTwister();
			for (int i = 0; i < 1000; i++)
			{
				lbNotSorted.Items.Add(mt.Next(0, 4000));
			}

			comboAlgorithms.Items.Add("Quick Sort");
			comboAlgorithms.Items.Add("Merge Sort");
			comboAlgorithms.Items.Add("Heap Sort");
			comboAlgorithms.Items.Add("Bubble Sort");
			comboAlgorithms.SelectedIndex = 0;
		}

		private void btnSort_Click(object sender, EventArgs e)
		{
			Sorters sort = new Sorters();
			List<int> list = lbNotSorted.Items.ToListOfType<int>();
			List<int> ret = new List<int>();
			DateTime start = DateTime.Now, stop = DateTime.Now;

			start = DateTime.Now;
			switch (comboAlgorithms.SelectedItem.ToString())
			{
				case "Quick Sort":
					{
						ret = sort.QuickSort(list, 0, list.Count - 1).ToListOfType<int>();
						break;
					}
				case "Merge Sort":
					{
						ret = sort.MergeSort(list, 0, list.Count - 1).ToListOfType<int>();
						break;
					}
				case "Heap Sort":
					{
						ret = sort.HeapSort(list).ToListOfType<int>();
						break;
					}
				case "Bubble Sort":
					{
						ret = sort.BubbleSort(list).ToListOfType<int>();
						break;
					}
			}
			stop = DateTime.Now;
			lbSorted.Items.Clear();
			lbSorted.Items.AddRange(ret.Cast<object>().ToArray());
			lblElapsed.Text = String.Format("{0} ticks", (stop - start).Ticks);
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				lblPath.Text = ofd.FileName;
			}
		}

		private void btnClean_Click(object sender, EventArgs e)
		{
			lbSorted.Items.Clear();
			lblElapsed.Text = "";
		}

		private void btnReseed_Click(object sender, EventArgs e)
		{
			int min = (int)nudMin.Value, max = (int)nudMax.Value;
			if (!(min == max || (min == 0 && max == 0)))
			{
				lbNotSorted.Items.Clear();
				MersenneTwister mt = new MersenneTwister();
				for (int i = 0; i < 1000; i++)
				{
					lbNotSorted.Items.Add(mt.Next(min, max));
				}
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			Mail.SendMail(txtFrom.Text, txtTo.Text, null, null, txtSubject.Text, MailPriority.High, txtBody.Text, txtServer.Text, false, null);
		}

		private void bgwTextFileParser_DoWork(object sender, DoWorkEventArgs e)
		{
			string path = e.Argument.ToString();
			int retCode = 0;
			List<DataInformation> lstdi = new List<DataInformation>();
			lstdi.Add(new DataInformation() { Key = "ID", Position = 0 });
			lstdi.Add(new DataInformation() { Key = "Via", Position = 1 });
			lstdi.Add(new DataInformation() { Key = "Civico", Position = 2 });
			lstdi.Add(new DataInformation() { Key = "Esponente", Position = 3 });
			lstdi.Add(new DataInformation() { Key = "CAP", Position = 4 });
			lstdi.Add(new DataInformation() { Key = "Comune", Position = 5 });
			lstdi.Add(new DataInformation() { Key = "Frazione", Position = 6 });
			lstdi.Add(new DataInformation() { Key = "Provincia", Position = 7 });
			TextFileParser tfp = new TextFileParser(ContentType.Path, path, lstdi);
			List<DataContainer> lstdc = tfp.ExtractData<DataContainer>(out retCode);
			Result ret = new Result()
			{
				lstdc = lstdc,
				Logs = tfp.Logs,
				retCode = retCode
			};
			e.Result = ret;
		}

		private void bgwTextFileParser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Result ret = e.Result as Result;
			dgvFile.DataSource = ret.lstdc;
			lblRetCode.Text = ret.retCode.ToString();
			txtLogs.Text = ret.Logs.ToString<Log>();
			btnStartParse.Enabled = true;

			MessageBox.Show("Parsing completed!!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnStartParse_Click(object sender, EventArgs e)
		{
			btnStartParse.Enabled = false;
			bgwTextFileParser.RunWorkerAsync(lblPath.Text);
		}
	}

	public class Result
	{
		public List<DataContainer> lstdc { get; set; }
		public int retCode { get; set; }
		public List<Log> Logs { get; set; }
	}

	public class DataContainer
	{
		public int ID { get; set; }
		public string Via { get; set; }
		public string Civico { get; set; }
		public string Esponente { get; set; }
		public string CAP { get; set; }
		public string Comune { get; set; }
		public string Frazione { get; set; }
		public string Provincia { get; set; }
	}
}
