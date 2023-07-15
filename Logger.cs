using System;
using System.IO;

namespace Halo5Reqs
{
	public class Logger : IDisposable, ILogger
	{
		private readonly StreamWriter _writer;

		public Logger()
		{
			String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Halo5Reqs\\log.txt");
			FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete);
			_writer = new StreamWriter(stream);
		}

		public void Log(String message)
		{
			_writer.WriteLine(message);
			_writer.Flush();
		}

		public void Dispose()
		{
			_writer.Dispose();
		}
	}
}
