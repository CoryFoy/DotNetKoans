using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotNetKoans.CSharp
{
	public class AboutDelegates : Koan
	{
		//A delegate is an object which lets you reference methods with the same signature and return type.
		//Delegates are types just like classes
		delegate int BinaryOp(int lhs, int rhs);

		private int Add(int lhs, int rhs)
		{
			return lhs + rhs;
		}
		[Koan(1)]
		private void DelegatesReferenceAMethod()
		{
			BinaryOp op = Add;
			Assert.Equal(FILL_ME_IN, op(3, 3));
		}
	}
}
