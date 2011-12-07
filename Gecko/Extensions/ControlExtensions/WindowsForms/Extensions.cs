using System.Windows.Forms;

namespace Gecko.Extensions.ControlExtensions.WindowsForms
{
	public static class Extensions
	{
		public static T FindControl<T>(this Control root, string name)
		{
			T ret = default(T);
			object[] temparr = root.Controls.Find(name, false);
			object temp = (temparr.Length > 0 ? temparr[0] : default(object));
			ret = (T)temp;
			return ret;
		}

		public static T FindControlRecursive<T>(this T root, string name) where T : Control
		{
			T ret;
			if (root.Name == name) return (T)((object)root);
			foreach (T c in root.Controls)
				if ((ret = c.FindControlRecursive<T>(name)) != null) return ret;
			return default(T);
		}
	}
}
