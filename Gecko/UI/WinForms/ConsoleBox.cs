using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gecko.UI.WinForms
{
	/// <summary>
	/// Autore: Alessandro Accardo.
	/// 
	/// Consente all'utente di immettere testo e fornisce funzionalità di modifica su più righe, la possibilità di nascondere i caratteri della password, la possibilità di AutoScroll del testo immesso e la possibilità di utilizzare i metodi AppendText e Clear e la proprietà Text in multithreading.
	/// </summary>
	[Description("Consente all'utente di immettere testo e fornisce funzionalità di modifica su più righe, la possibilità di nascondere i caratteri della password, la possibilità di AutoScroll del testo immesso e la possibilità di utilizzare i metodi AppendText e Clear e la proprietà Text in multithreading.")]
	[ToolboxBitmap(typeof(TextBox))]
	public class ConsoleBox : TextBox
	{
		public ConsoleBox()
		{
			InitializeComponent();
		}

		#region Codice Generato del Designer
		/// <summary> 
		/// Variabile di progettazione necessaria.
		/// </summary>
		private IContainer components = null;

		/// <summary> 
		/// Liberare le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ConsoleBox
			// 
			this.Name = "ConsoleBox";
			this.ResumeLayout();
		}
		#endregion

		#region Import DLL user32.dll
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
		#endregion

		#region AutoScroll Property Definition
		private bool _autoScroll = true;

		/// <summary>
		/// Consente di specificare se, all'inserimento di nuove righe, il controllo deve effettuare uno scrolling automatico del contenuto.
		/// </summary>
		[Description("Consente di specificare se, all'inserimento di nuove righe, il controllo deve effettuare uno scrolling automatico del contenuto.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AutoScrollText
		{
			get
			{
				return _autoScroll;
			}
			set
			{
				_autoScroll = value;
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (AutoScrollText)
			{
				SelectionStart = TextLength;
				ScrollToCaret();
			}
		}
		#endregion

		#region Text / AppendText
		private delegate void TextDelegate(string text);

		private void SafeSetText(string text)
		{
			base.Text = text;
		}
		/// <summary>
		/// Ottiene o imposta il testo corrente nell'oggetto ConsoleBoxLibrary.ConsoleBox.
		/// </summary>
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (InvokeRequired)
				{
					TextDelegate callback = SafeSetText;
					BeginInvoke(callback, new object[] { value });
				}
				else
					base.Text = value;
			}
		}

		/// <summary>
		/// Accoda testo a quello corrente di una casella di testo.
		/// </summary>
		/// <param name="AppendingText">
		/// Il testo da aggiungere al contenuto corrente della casella di testo.
		/// </param>
		public new void AppendText(string AppendingText)
		{
			if (InvokeRequired)
			{
				TextDelegate callback = AppendText;
				BeginInvoke(callback, new object[] { AppendingText });
				return;
			}

			if (AppendingText.Length > 0)
			{
				SelectionStart = TextLength;
				ScrollToCaret();
				SendMessage(new HandleRef(this, Handle), 0xc2, 0, AppendingText);
				ClearUndo();
			}
			if (!AutoScrollText)
			{
				SelectionStart = 0;
				ScrollToCaret();
			}
		}
		#endregion

		#region Clear method
		private delegate void VoidDelegate();

		/// <summary>
		/// Cancella tutto il testo dal controllo casella di testo.
		/// </summary>
		public new void Clear()
		{
			if (InvokeRequired)
			{
				VoidDelegate callback = Clear;
				BeginInvoke(callback, new object[] { });
			}
			else
				base.Clear();
		}
		#endregion
	}
}
