using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable
// ReSharper disable NotAccessedVariable
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedField.Local
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable ConstantNullCoalescingCondition
// ReSharper disable JoinNullCheckWithUsage
// ReSharper disable UnusedTupleComponentInReturnValue
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable ArrangeMethodOrOperatorBody
#pragma warning disable 219
#pragma warning disable IDE0020 // Use pattern matching
#pragma warning disable IDE0018 // Inline variable declaration

namespace JJ.Demos.Misc
{
	[TestClass]
	public class CSharp7Tests
	{
		[TestMethod]
		public void Test_CSharp7_BinaryLiterals_Old()
		{
			int x = 0x3DA8AEBB;
		}

		[TestMethod]
		public void Test_CSharp7_BinaryLiterals_New()
		{
			int x = 0b0111101101010001010111010111011;
		}

		[TestMethod]
		public void Test_CSharp7_DigitSeparators_Old()
		{
			long x = 15466234543;
		}

		[TestMethod]
		public void Test_CSharp7_DigitSeparators_New()
		{
			long x = 15_466_234_543;
		}

		[TestMethod]
		public void Test_CSharp7_LocalFunctions()
		{
			int result = localFunction(12);

			int localFunction(int input) => input + 10;
		}

		[TestMethod]
		public void Test_CSharp7_OutVariables_Old()
		{
			double x;
			double y;
			GetXAndY(out x, out y);
		}

		[TestMethod]
		public void Test_CSharp7_OutVariables_New()
		{
			GetXAndY(out double x, out double y);
		}

		private void GetXAndY(out double x, out double y)
		{
			x = 10;
			y = 12;
		}

		[TestMethod]
		public void Test_CSharp7_PatternMatching_Old()
		{
			object obj = null;
			if (obj is int)
			{
				int myInt = (int)obj;

				myInt += 2;
			}
		}

		[TestMethod]
		public void Test_CSharp7_PatternMatching_New()
		{
			object obj = null;
			if (obj is int myInt)
			{
				myInt += 2;
			}
		}

		private object _myObject;

		[TestMethod]
		public void Test_CSharp7_ThrowExpressions_Old()
		{
			object myObject = new object();

			_myObject = myObject ?? throw new ArgumentNullException(nameof(myObject));
		}

		[TestMethod]
		public void Test_CSharp7_ThrowExpressions_New()
		{
			object myObject = new object();

			if (myObject == null) throw new ArgumentNullException(nameof(myObject));
			_myObject = myObject;
		}

		[TestMethod]
		public void Test_CSharp7_TuplesAndDiscards_Old()
		{
			Tuple<int, int, int> tuple = GetTuple_Old();
			int x = tuple.Item1;
		}

		private Tuple<int, int, int> GetTuple_Old()
		{
			var x = new Tuple<int, int, int>(10, 20, 30);
			return x;
		}

		[TestMethod]
		public void Test_CSharp7_TuplesAndDiscards_New()
		{
			(int x, _, _) = GetTuple_New();
		}

		private (int, int, int) GetTuple_New()
		{
			var x = (10, 20, 30);
			return x;
		}

		private int _int;

		private int MyExpressionBodiedAccessors_Old
		{
			get { return _int; }
			set { _int = value; }
		}

		private int MyExpressionBodiedAccessors_New
		{
			get => _int;
			set => _int = value;
		}

		/// <summary> Expression bodied constructor </summary>
		public CSharp7Tests() => _int = 10;

		/// <summary> Expression bodied finalizer </summary>
		~CSharp7Tests() => _int = 0;

		// TODO:
		// Deconstruction
		// Ref returns and locals
		// Generalized async return types
	}
}
