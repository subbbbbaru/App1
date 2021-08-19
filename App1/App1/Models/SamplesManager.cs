using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace App1.Models
{
	public static class SamplesManager
	{
		static SamplesManager()
		{

		}

		public static event Action<string> OpenFile;

		public static void OnOpenFile(string path)
		{
			OpenFile?.Invoke(path);
		}

	}
}
