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

			using (Logger logger = new Logger())
			{
				MainForm mainForm;
				using (LoginForm form = new LoginForm(logger))
				{
					if (form.ShowDialog() != DialogResult.OK)
						return;
					mainForm = form.MainForm;
				}

				Application.Run(mainForm);
			}
		}
	}
}
