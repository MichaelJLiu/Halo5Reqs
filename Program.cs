using System;
using System.Windows.Forms;

namespace Halo5Reqs
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MainForm mainForm;
			using (LoginForm form = new LoginForm())
			{
				if (form.ShowDialog() != DialogResult.OK)
					return;
				mainForm = form.MainForm;
			}

			Application.Run(mainForm);
		}
	}
}
