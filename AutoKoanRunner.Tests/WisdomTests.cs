using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AutoKoanRunner.Core;

namespace AutoKoanRunner.Tests
{
	public class WisdomTests
	{
		[Fact]
		public void StateOfEnlightenment_EmptyAnalysis_NotEnlightend()
		{
			var a = new Analysis();
			a.CompletedKoans = 0;
			a.TotalKoans = 5;

			Assert.Equal("You have not yet reached enlightenment.", Master.StateOfEnlightenment(a));
		}
		[Fact]
		public void StateOfEnlightenment_CompletedEqualsTotal_Enlightend()
		{
			Analysis a = new Analysis();
			a.CompletedKoans = 5;
			a.TotalKoans = 5;

			Assert.Equal("You have reached enlightenment.", Master.StateOfEnlightenment(a));
		}
		[Fact]
		public void Encouragement_NoProgress_EmptyString()
		{
			Analysis a = new Analysis();

			Assert.Equal(String.Empty, Master.Encouragement(a));
		}
		[Fact]
		public void Encouragement_CompletedEqualsTotal_EmptyString()
		{
			Analysis a = new Analysis();
			a.CompletedKoans = 5;
			a.TotalKoans = 5;

			Assert.Equal(String.Empty, Master.Encouragement(a));
		}
		[Fact]
		public void Encouragement_CompletedNotEqualsTotalAttemptsZero_ProgressingCount()
		{
			Analysis a = new Analysis();
			a.CompletedKoans = 1;
			a.TotalKoans = 5;
			a.FailedAttempts = 0;

			Assert.Equal("You are progressing. Excellent. 1 completed.", Master.Encouragement(a));
		}
		[Fact]
		public void Encouragement_CompletedNotEqualsTotalAttemptsOne_DontGiveUp()
		{
			Analysis a = new Analysis();
			a.CompletedKoans = 1;
			a.TotalKoans = 5;
			a.FailedAttempts = 1;

			Assert.Equal("Do not lose hope.", Master.Encouragement(a));
		}
		[Fact]
		public void Encouragement_CompletedNotEqualsTotalAttemptsThree_SeekHelp()
		{
			Analysis a = new Analysis();
			a.CompletedKoans = 1;
			a.TotalKoans = 5;
			a.FailedAttempts = 3;

			Assert.Equal("I sense frustration. Do not be afraid to ask for help.", Master.Encouragement(a));
		}
		[Fact]
		public void WhereToSeek_FailedKoan_ArrayOfLines()
		{
			var output = "\r\n\r\n*******************************************************************\r\n*******************************************************************\r\nDotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness\r\nDotNetKoans.CSharp.AboutAsserts.AssertTruthWithMessage has expanded your awareness\r\nDotNetKoans.CSharp.AboutAsserts.AssertEquality has expanded your awareness\r\nThe test DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality has damaged your karma. The following stack trace has been declared to be at fault\r\nAssert.Equal() Failure\r\nExpected: 3\r\nActual:   2\r\n   at DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality() in C:\\Users\\jaargero\\DotNetKoans\\CSharp\\AboutAsserts.cs:line 36\r\n<test name=\"DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality\" type=\"DotNetKoans.CSharp.AboutAsserts\" method=\"ABetterWayOfAssertingEquality\" result=\"Fail\" time=\"0.098\"><failure exception-type=\"Xunit.Sdk.EqualException\"><message>Assert.Equal() Failure\r\nExpected: 3\r\nActual:   2</message><stack-trace>   at DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality() in C:\\Users\\jaargero\\DotNetKoans\\CSharp\\AboutAsserts.cs:line 36</stack-trace></failure></test>\r\nKoan progress:(3/5),(0/5),(0/6),(0/3),(0/6),(0/23),(0/9),(0/11),(0/16),(0/23),(0/7),\r\n*******************************************************************\r\n*******************************************************************\r\n\r\n\r\n";
			var sut = new Master("CSharp");

			string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			Assert.Equal(new string[] { "Assert.Equal() Failure", "Expected: 3", "Actual:   2" }, sut.WhereToSeek(lines));
		}
		[Fact]
		public void WhatToMedidateOn_FailedKoan_FirstLineOfStackTrace()
		{
			var output = "\r\n\r\n*******************************************************************\r\n*******************************************************************\r\nDotNetKoans.CSharp.AboutAsserts.AssertTruth has expanded your awareness\r\nDotNetKoans.CSharp.AboutAsserts.AssertTruthWithMessage has expanded your awareness\r\nDotNetKoans.CSharp.AboutAsserts.AssertEquality has expanded your awareness\r\nThe test DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality has damaged your karma. The following stack trace has been declared to be at fault\r\nAssert.Equal() Failure\r\nExpected: 3\r\nActual:   2\r\n   at DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality() in C:\\Users\\jaargero\\DotNetKoans\\CSharp\\AboutAsserts.cs:line 36\r\n<test name=\"DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality\" type=\"DotNetKoans.CSharp.AboutAsserts\" method=\"ABetterWayOfAssertingEquality\" result=\"Fail\" time=\"0.098\"><failure exception-type=\"Xunit.Sdk.EqualException\"><message>Assert.Equal() Failure\r\nExpected: 3\r\nActual:   2</message><stack-trace>   at DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality() in C:\\Users\\jaargero\\DotNetKoans\\CSharp\\AboutAsserts.cs:line 36</stack-trace></failure></test>\r\nKoan progress:(3/5),(0/5),(0/6),(0/3),(0/6),(0/23),(0/9),(0/11),(0/16),(0/23),(0/7),\r\n*******************************************************************\r\n*******************************************************************\r\n\r\n\r\n";

			string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			Assert.Equal("at DotNetKoans.CSharp.AboutAsserts.ABetterWayOfAssertingEquality() in C:\\Users\\jaargero\\DotNetKoans\\CSharp\\AboutAsserts.cs:line 36" , Master.WhatToMeditateOn(lines));
		}
	}
}
