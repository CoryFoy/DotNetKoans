using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace AutoKoanRunner
{
	class Program
	{
		//private static readonly string koansSource = @"..\..\..\CSharp";
		//private static readonly string koansAssembly = @"..\..\..\CSharp\bin\debug\csharp.dll";
		private static readonly string koansRunner = @"..\..\..\KoanRunner\bin\debug\koanrunner.exe";
		private static DateTime lastChange;
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
				Array.ForEach(watchers, w => {
					w.Changed += StartRunner;
					w.NotifyFilter = NotifyFilters.LastWrite;
					w.EnableRaisingEvents = true;
				});

				
				//Auto run the first time
				Array.ForEach(KoanSource.Sources, s => {
					StartRunner(null, new FileSystemEventArgs(WatcherChangeTypes.Changed, s.SourceFolder, s.Extension));
					lastChange = DateTime.MinValue;
				});

				Console.WriteLine("When you save a Koan, we'll check your work.");
				Console.WriteLine("Press a key to exit...");
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
		private static void StartRunner(object sender, FileSystemEventArgs e)
		{
			if (e != null)
			{
				DateTime timestamp = File.GetLastWriteTime(e.FullPath);
				if (lastChange.ToString() == timestamp.ToString())// Use string version to eliminate second save by VS a fraction of a second later
					return;
				lastChange = timestamp;
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
			const string kExpanded = "has expanded your";
			const string kDamaged = "has damaged your";
			Array.ForEach(lines, line =>
			{
				if (line.Contains(kExpanded))
				{
					PrintTestLine(line, ConsoleColor.Green, kExpanded, projectName);
				}
				else if (line.Contains(kDamaged))
				{
					PrintTestLine(line, ConsoleColor.Red, kDamaged, projectName);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine(line);
				}
			});
		}
		private static void PrintTestLine(string line, ConsoleColor accent, string action, string projectName)
		{
			string koanAssembly = String.Format(".{0}.", projectName);
			int testStart = line.IndexOf(koanAssembly) + koanAssembly.Length;
			int testEnd = line.IndexOf(action, testStart);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(line.Substring(0, testStart));
			Console.ForegroundColor = accent;
			Console.Write(line.Substring(testStart, testEnd - testStart));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(line.Substring(testEnd));
		}
	}
}
