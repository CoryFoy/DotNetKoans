using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoKoanRunner.Core
{
	public class Analysis
	{
		public string LastPassedKoan { get; set; }
		public int CompletedKoans { get; set; }
		public int TotalKoans { get; set; }
        public string FailedKoan { get; set; }
		public int FailedAttempts { get; set; }
		public string ProgressBar { get; set; }
	}
}
