using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoKoanRunner.Core
{
	public class Master
	{
		public const string kExpanded = "has expanded your awareness";
		public const string kDamaged = "has damaged your karma.";
		readonly string _projectName;

        public Master(string projectName)
		{
			_projectName = projectName;
		}
		public Analysis Analyze(string[] lines, Analysis prior)
		{
            var result = new Analysis();
			result.LastPassedKoan = FindKoan(lines, _projectName, kExpanded);
			result.FailedKoan = FindKoan(lines, _projectName, kDamaged);
			result.CompletedKoans = CountCompleted(lines);
			result.FailedAttempts = ComputeAttempts(result.FailedKoan, prior);
			int total;
			result.ProgressBar = ComputeProgress(lines, out total);
			result.TotalKoans = total;
			return result;
        }
		public static string StateOfEnlightenment(Analysis a)
		{
			return a.CompletedKoans == a.TotalKoans ? "You have reached enlightenment." : "You have not yet reached enlightenment.";
		}
		public static string Encouragement(Analysis a)
		{
			if ((a.CompletedKoans == 0 && a.FailedAttempts == 0) || a.CompletedKoans == a.TotalKoans)
			{
				return String.Empty;
			}
			if (a.CompletedKoans > 0 && a.FailedAttempts == 0)
			{
				return string.Format("You are progressing. Excellent. {0} completed.", a.CompletedKoans);
			}
			if (a.FailedAttempts < 3)
			{
				return "Do not lose hope.";
			}
			return "I sense frustration. Do not be afraid to ask for help.";
		}
		public static string[] WhereToSeek(string[] lines)
		{
			string[] result = new string[] { };
			int damaged = Array.FindIndex(lines, l => l.Contains(kDamaged));
			if (damaged >= 0)
			{
				int start = damaged + 1;
				int end = Array.FindIndex(lines, start, l => l.TrimStart().StartsWith("at")) ;
				result = new string[end - start];
				Array.Copy(lines, start, result, 0, end - start);
			}
			return result;
		}
		public static string WhatToMeditateOn(string[] lines)
		{
			return Array.Find(lines, l => l.TrimStart().StartsWith("at")).TrimStart();
		}
        private static string FindKoan(string[] lines, string projectName, string action)
		{
			int lastPassingOffset = Array.FindLastIndex(lines, l => l.Contains(action));
			if (lastPassingOffset < 0)
				return String.Empty;
			string passing = lines[lastPassingOffset];
			int start = passing.IndexOf(projectName);
			int end = passing.IndexOf(action);
			return passing.Substring(start, end - start - 1);
		}
		private static int CountCompleted(string[] lines)
		{
			return Array.FindAll(lines, l => l.Contains(kExpanded)).Length;
		}
		private static int ComputeAttempts(string failedKoan, Analysis prior)
		{
			return failedKoan == prior.FailedKoan ? prior.FailedAttempts + 1 : 0;
		}
		private static string ComputeProgress(string[] lines, out int total)
		{
			int totalCompleted = 0;
			total = 0;
			StringBuilder visual = new StringBuilder();
			string progressLine = Array.Find(lines, l => l.Contains("Koan progress:"));
			if (string.IsNullOrEmpty(progressLine) == false)
			{
				foreach (string pair in progressLine.Substring(progressLine.IndexOf(":") + 1).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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
			}
			return visual.ToString();
		}
	}
}
