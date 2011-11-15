using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace AutoKoanRunner
{
	class Program
	{
		private static readonly string koansSource = @"..\..\..\CSharp";
		private static readonly string koansAssembly = @"..\..\..\CSharp\bin\debug\csharp.dll";
		private static readonly string koansRunner = @"..\..\..\KoanRunner\bin\debug\koanrunner.exe";
		private static DateTime lastChange;
        static void Main(string[] args)
		{
			if (Directory.Exists(koansSource) == false)
			{
				Console.WriteLine("The Koans were not where we expecte them to be.");
				return;
			}
			using (FileSystemWatcher watcher = new FileSystemWatcher(koansSource, "*.cs"))
			{
				watcher.Changed += StartRunner;
				watcher.NotifyFilter = NotifyFilters.LastWrite;
				watcher.EnableRaisingEvents = true;

				lastChange = DateTime.MinValue;

				StartRunner(null, null);//Auto run the first time

				Console.WriteLine("When you save a Koan, we'll check your work.");
				Console.WriteLine("Press a key to exit...");
				Console.ReadKey();
				watcher.Changed -= StartRunner;
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
			BuildProject();
			RunKoans();
		}
		private static bool BuildProject()
		{
			Console.WriteLine("Building...");
			using (Process build = new Process())
			{
				build.StartInfo.FileName = "devenv";
				build.StartInfo.Arguments = @"/build Debug /project CSharp ..\..\..\DotNetKoans.sln";
				build.StartInfo.CreateNoWindow = true;
				build.Start();
				build.WaitForExit();
			}
			return false;
		}
		private static void RunKoans()
		{
			if (File.Exists(koansAssembly))
			{
				Console.WriteLine("Checking Koans...");
				using (Process launch = new Process())
				{
					launch.StartInfo.FileName = koansRunner;
					launch.StartInfo.Arguments = koansAssembly;
					launch.StartInfo.RedirectStandardOutput = true;
					launch.StartInfo.UseShellExecute = false;
					launch.Start();
					string output = launch.StandardOutput.ReadToEnd();
					launch.WaitForExit();
					EchoResult(output);
				}
			}
			File.Delete(koansAssembly);
		}
		private static void EchoResult(string output)
		{
			string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			const string kExpanded = "has expanded your";
			const string kDamaged = "has damaged your";
			Array.ForEach(lines, line =>
			{
				if (line.Contains(kExpanded))
				{
					PrintTestLine(line, ConsoleColor.Green, kExpanded);
				}
				else if (line.Contains(kDamaged))
				{
					PrintTestLine(line, ConsoleColor.Red, kDamaged);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine(line);
				}
			});
		}
		private static void PrintTestLine(string line, ConsoleColor accent, string action)
		{
			const string kKoanAssembly = ".CSharp.";
			int testStart = line.IndexOf(kKoanAssembly) + kKoanAssembly.Length;
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
