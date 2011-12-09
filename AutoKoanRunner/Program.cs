using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace AutoKoanRunner
{
	class Program
	{
		//private static readonly string koansSource = @"..\..\..\CSharp";
		//private static readonly string koansAssembly = @"..\..\..\CSharp\bin\debug\csharp.dll";
		private static readonly string koansRunner = @"..\..\..\KoanRunner\bin\debug\koanrunner.exe";
		private static DateTime _LastChange;
		private static string _PriorFailed;
		private static int _Attempts;
        internal class KoanSource
		{
			public string Extension { get; set; }
			public string ProjectName { get; set; }
			public string SourceFolder { get; set; }
			public string AssemblyPath { get; set; }
			public static readonly KoanSource CSharp = new KoanSource
			{
				Extension = ".cs",
				ProjectName = "CSharp",
				SourceFolder = @"..\..\..\CSharp",
				AssemblyPath = @"..\..\..\CSharp\bin\debug\csharp.dll"
			};
			public static readonly KoanSource VBasic = new KoanSource
			{
				Extension = ".vb",
				ProjectName = "VBNet",
				SourceFolder = @"..\..\..\VBNet",
				AssemblyPath = @"..\..\..\VBNet\bin\debug\VBNet.dll"
			};
			public static readonly KoanSource[] Sources = new[] { CSharp, VBasic };
		}
		static void Main(string[] args)
		{
			if (Array.TrueForAll(KoanSource.Sources, source => Directory.Exists(source.SourceFolder)) == false)
			{
				Console.WriteLine("The Koans were not where we expecte them to be.");
				return;
			}
			FileSystemWatcher[] watchers = Array.ConvertAll(
				KoanSource.Sources,
				source => new FileSystemWatcher(source.SourceFolder, "*" + source.Extension));
			try
			{
				Array.ForEach(watchers, w =>
				{
					w.Changed += StartRunner;
					w.NotifyFilter = NotifyFilters.LastWrite;
					w.EnableRaisingEvents = true;
				});


				//Auto run the first time
				Array.ForEach(KoanSource.Sources, s =>
				{
					StartRunner(null, new FileSystemEventArgs(WatcherChangeTypes.Changed, s.SourceFolder, s.Extension));
					ResetLastRunData();
				});

				Console.WriteLine("When you save a Koan, the Master will again ponder your work.");
				Console.WriteLine("Press a key to exit...");
				Console.WriteLine();
				Console.ReadKey();
			}
			finally
			{
				Array.ForEach(watchers, w =>
				{
					w.Changed -= StartRunner;
					w.Dispose();
				});
			}
		}
		private static void ResetLastRunData()
		{
			_LastChange = DateTime.MinValue;
			_PriorFailed = String.Empty;
			_Attempts = 0;
		}
		private static void StartRunner(object sender, FileSystemEventArgs e)
		{
			if (e != null)
			{
				DateTime timestamp = File.GetLastWriteTime(e.FullPath);
				if (_LastChange.ToString() == timestamp.ToString())// Use string version to eliminate second save by VS a fraction of a second later
					return;
				_LastChange = timestamp;
			}
			KoanSource source = Array.Find(KoanSource.Sources, s => e.FullPath.EndsWith(s.Extension));
			BuildProject(source);
			RunKoans(source);
		}
		private static bool BuildProject(KoanSource koans)
		{
			Console.WriteLine("Building...");
			using (Process build = new Process())
			{
				build.StartInfo.FileName = "devenv";
				build.StartInfo.Arguments = String.Format(@"/build Debug /project {0} ..\..\..\DotNetKoans.sln", koans.ProjectName);
				build.StartInfo.CreateNoWindow = true;
				build.Start();
				build.WaitForExit();
			}
			return false;
		}
		private static void RunKoans(KoanSource koans)
		{
			if (File.Exists(koans.AssemblyPath))
			{
				Console.WriteLine("Checking Koans...");
				using (Process launch = new Process())
				{
					launch.StartInfo.FileName = koansRunner;
					launch.StartInfo.Arguments = koans.AssemblyPath;
					launch.StartInfo.RedirectStandardOutput = true;
					launch.StartInfo.UseShellExecute = false;
					launch.Start();
					string output = launch.StandardOutput.ReadToEnd();
					launch.WaitForExit();
					EchoResult(output, koans.ProjectName);
				}
			}
			File.Delete(koans.AssemblyPath);
		}
		private static void EchoResult(string output, string projectName)
		{
			string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			const string kExpanded = "has expanded your awareness";
			const string kDamaged = "has damaged your karma.";
			PrintLastActions(projectName, lines, kExpanded, kDamaged);
			PrintMastersComments(lines, kExpanded, kDamaged);
			PrintAnswersYouSeek(lines, kDamaged);
			PrintFinalWords(lines);
		}
		private static void PrintLastActions(string projectName, string[] lines, string kExpanded, string kDamaged)
		{
			int lastSuccess = Array.FindLastIndex(lines, l => l.Contains(kExpanded));
			if (lastSuccess >= 0)
			{
				PrintTestLineJustTest(lines[lastSuccess], ConsoleColor.Green, kExpanded, projectName);
			}
			int lastFail = Array.FindLastIndex(lines, l => l.Contains(kDamaged));
			if (lastFail >= 0)
			{
				string method = PrintTestLineJustTest(lines[lastFail], ConsoleColor.Red, kDamaged, projectName);
				CaptureTriesData(method);
			}
		}
		private static void CaptureTriesData(string method)
		{
			if (method != _PriorFailed)
			{
				_PriorFailed = method;
				_Attempts = 0;
			}
			else
			{
				_Attempts++;
			}
		}
		private static void PrintMastersComments(string[] lines, string kExpanded, string kDamaged)
		{
			Console.WriteLine();
			Console.WriteLine("The Master says:");
			Console.ForegroundColor = ConsoleColor.Cyan;
			if (Array.FindIndex(lines, l => l.Contains(kDamaged)) >= 0)
			{
				Console.WriteLine("\tYou have not yet reached enlightenment.");
			}
			int completed = Array.FindAll(lines, l => l.Contains(kExpanded)).Length;
			if (completed == 0 && _Attempts == 0)
			{
            	//Nothing
            }
			else if (completed > 0 && _Attempts == 0)
			{
				Console.WriteLine("\tYou are progressing. Excellent. {0} completed.", completed);
			}
			else if (_Attempts < 3)
			{
				Console.WriteLine("\tDo not lose hope.");
			}
			else
			{
				Console.WriteLine("\tI sense frustration. Do not be afraid to ask for help.");
			}
			Console.ForegroundColor = ConsoleColor.White;
		}
		private static void PrintAnswersYouSeek(string[] lines, string kDamaged)
		{
			Console.WriteLine();
			Console.WriteLine("The answers you seek...");
			int damaged = Array.FindIndex(lines, l => l.Contains(kDamaged));
			Console.ForegroundColor = ConsoleColor.Red;
			int offset = damaged + 1;
            for (; lines[offset].TrimStart().StartsWith("at") == false; offset++)
			{
				Console.WriteLine("\t{0}", lines[offset]);
			}
			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine();
			Console.WriteLine("Please meditate on the following code:");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\t{0}", lines[offset].TrimStart());
			Console.ForegroundColor = ConsoleColor.White;
		}
		private static void PrintFinalWords(string[] lines)
		{
			Console.WriteLine();
			Console.WriteLine("sleep is the best meditation");
			int totalCompleted = 0;
			string progressLine = Array.Find(lines, l => l.Contains("Koan progress:"));
			int total = 0;
			StringBuilder visual = new StringBuilder();
			foreach (string pair in progressLine.Substring(progressLine.IndexOf(":") + 1).Split(new[]{','},StringSplitOptions.RemoveEmptyEntries))
			{
				int slash = pair.IndexOf('/', 2);
				int n = int.Parse(pair.Substring(1, slash - 1));
				int m = int.Parse(pair.Substring(slash + 1, pair.Length - slash - 2));
				totalCompleted += n;
				total += m;
				if (n == 0)
					visual.Append('_');
				else if (n != m)
					visual.Append('X');
				else
					visual.Append('.');
			};
			Console.WriteLine("your path thus far [{0}] {1}/{2}", visual.ToString(), totalCompleted, total);
		}
		private static string PrintTestLineJustTest(string line, ConsoleColor accent, string action, string projectName)
		{
			string koanAssembly = String.Format(".{0}.", projectName);
			int testStart = line.IndexOf(koanAssembly) + koanAssembly.Length;
			int testEnd = line.IndexOf(action, testStart);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("{0}.", projectName);
			Console.ForegroundColor = accent;
			string methodName = line.Substring(testStart, testEnd - testStart);
			Console.Write(methodName);
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(action);
			return methodName;
		}
	}
}
