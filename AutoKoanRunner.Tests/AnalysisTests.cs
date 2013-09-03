using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AutoKoanRunner.Core;

namespace AutoKoanRunner.Tests
{
	public class AnalysisTests
	{
		[Fact]
		public void LastPassedKoan_EmptySet_NoProgress()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[0], new Analysis());

			Assert.Equal(String.Empty, a.LastPassedKoan);
		}
		[Fact]
		public void LastPassedKoan_OnePassed_LastPassedIdentified()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[] { "DotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness" }, new Analysis());

			Assert.Equal("CSharp.AboutAsserts.AssertTruth", a.LastPassedKoan);
		}
		[Fact]
		public void LastPassedKoan_TwoPassed_LastPassedIdentified()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[] {
				"DotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness",
				"DotNetKoans.CSharp.AboutAsserts.AssertTruthWithMessage has expanded your awareness" 
			}, new Analysis());

			Assert.Equal("CSharp.AboutAsserts.AssertTruthWithMessage", a.LastPassedKoan);
		}
		[Fact]
		public void FailedKoan_EmptySet_NoProgress()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[0], new Analysis());

			Assert.Equal(String.Empty, a.FailedKoan);
		}
		[Fact]
		public void FailedKoan_OnePassedOneFailed_FailedIdentified()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[] {
				"DotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness",
				"The test DotNetKoans.CSharp.AboutAsserts.AssertTruthWithMessage has damaged your karma. The following stack trace has been declared to be at fault" 
			}, new Analysis());

			Assert.Equal("CSharp.AboutAsserts.AssertTruthWithMessage", a.FailedKoan);
		}
		[Fact]
		public void Completed_EmptySet_Zero()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[0], new Analysis());

			Assert.Equal(0, a.CompletedKoans);
		}
		[Fact]
		public void Completed_OnePassedOneFailed_One()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new string[] {
				"DotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness",
				"The test DotNetKoans.CSharp.AboutAsserts.AssertTruthWithMessage has damaged your karma. The following stack trace has been declared to be at fault" 
			}, new Analysis());

			Assert.Equal(1, a.CompletedKoans);
		}
		[Fact]
		public void Attempts_FirstTry_Zero()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new [] {
				"The test DotNetKoans.CSharp.AboutAsserts.AssertTruth has damaged your karma. The following stack trace has been declared to be at fault" 
			}, new Analysis());

			Assert.Equal(0, a.FailedAttempts);
		}
		[Fact]
		public void Attempts_SecondTry_Zero()
		{
			var sut = new Master("CSharp");
			var lines = new [] { "The test DotNetKoans.CSharp.AboutAsserts.AssertTruth has damaged your karma. The following stack trace has been declared to be at fault" };

			var a = sut.Analyze(lines, new Analysis());
			a = sut.Analyze(lines, a);

			Assert.Equal(1, a.FailedAttempts);
		}
		[Fact]
		public void ProgressBar_ZeroFiveZeroFiveZeroFive_ThreeUnderscores()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new[] { "Koan progress:(0/5),(0/5),(0/5)," }, new Analysis());

			Assert.Equal("___", a.ProgressBar);
		}
		[Fact]
		public void ProgressBar_OneFiveZeroFiveZeroFive_XTwoUnderscores()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new[] { "Koan progress:(1/5),(0/5),(0/5)," }, new Analysis());

			Assert.Equal("X__", a.ProgressBar);
		}
		[Fact]
		public void ProgressBar_FiveFiveOneFiveZeroFive_DotXUnderscore()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new[] { "Koan progress:(5/5),(1/5),(0/5)," }, new Analysis());

			Assert.Equal(".X_", a.ProgressBar);
		}
		[Fact]
		public void TotalKoans_ZeroFiveZeroFiveZeroFive_ThreeUnderscores()
		{
			var sut = new Master("CSharp");

			var a = sut.Analyze(new[] { "Koan progress:(0/5),(0/5),(0/5)," }, new Analysis());

			Assert.Equal(15, a.TotalKoans);
		}
	}
}
