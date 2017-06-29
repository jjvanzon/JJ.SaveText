﻿using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JJ.Framework.Testing;
using System.Globalization;
// ReSharper disable UnusedVariable
#pragma warning disable 162
#pragma warning disable 219

namespace JJ.Demos.Misc
{
    [TestClass]
    public class MiscTests
    {
        [TestMethod]
        public void Test_Linq_Take_MoreThanCollectionSize()
        {
            IList<int> source = new[] { 1, 2, 3 };
            IList<int> dest = source.Take(5).ToArray();
            AssertHelper.AreEqual(3, () => dest.Count);
        }

        [TestMethod]
        public void Test_CultureInfo_TextInfo_ListSeparator_Chinese_Etc()
        {
            CultureInfo chineseCulture = new CultureInfo("zh-CN");
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            CultureInfo dutchCulture = new CultureInfo("nl-NL");
            CultureInfo enUSCulture = new CultureInfo("en-US");

            // These are not exactly what you would expect in terms of punctuation, so in most cases there is no point using it.
            string chineseListSeparator = chineseCulture.TextInfo.ListSeparator;
            string russianListSeparator = russianCulture.TextInfo.ListSeparator;
            string dutchListSeparator = dutchCulture.TextInfo.ListSeparator;
            string enUSListSeparator = enUSCulture.TextInfo.ListSeparator;
        }

        [TestMethod]
        public void Test_Max_Simple()
        {
            int[] values = { 10, 13, -10, 0, 1, -1, 40, 20 };

            int count = values.Length;

            int max = int.MinValue;
            for (int i = 0; i < count; i++)
            {
                int value = values[i];

                if (max < value)
                {
                    max = value;
                }
            }
        }

        [TestMethod]
        public void Test_Max_With_Disappearing_Beginning()
        {
            int[] values = { 10, 13, -10, 0, 1, -1, 40, 20 };

            int count = values.Length;

            // Array index is the value index at which we start counting
            int[] maxArray = new int[count];

            for (int i = 0; i < count; i++)
            {
                int max = int.MinValue;

                for (int j = i; j < count; j++)
                {
                    int value = values[j];

                    if (max < value)
                    {
                        max = value;
                    }
                }

                maxArray[i] = max;
            }

            // If a value is added, you have to compare the max value in the arrays
            // with the new value.
        }

        [TestMethod]
        public void Test_Misc_NaNCheck_AfterNumberCheck()
        {
            const double value = double.NaN;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (value < 0.0)
            {
                // ReSharper disable once HeuristicUnreachableCode
                int bla = 0;
            }

            if (double.IsNaN(value))
            {
                int bla = 0;
            }
        }

        [TestMethod]
        public void Test_Misc_PlusOperatorOnStringsWithNull_NoException()
        {
            string str1 = null;
            string str2 = "bla";
            string str3 = "bla2";
            string str4 = str1 + str2 + str3;
        }

        [TestMethod]
        public void Test_Misc_CollectionInitializers()
        {
            var myCollection = new MyCollectionType
            {
                { 10, 10 }
            };

            foreach (int x in myCollection)
            { }
        }

        private class MyCollectionType : IEnumerable
        {
            private readonly int[] _underlyingArray = new int[2];

            //public IEnumerator<int> GetEnumerator()
            //{
            //    return ((IEnumerable<int>)_underlyingArray).GetEnumerator();
            //}

            IEnumerator IEnumerable.GetEnumerator() => _underlyingArray.GetEnumerator();

            //public void Add(int bla)
            //{ }

            public void Add(int bla, int bla2)
            { }
        }

        [TestMethod]
        public void Test_Misc_ForEach()
        {
            var myCollection = new MyCollectionType2();
            foreach (int x in myCollection)
            { }
        }

        private class MyCollectionType2
        {
            public bool MoveNext() => false;
            public int Current { get; set; }
            public IEnumerator GetEnumerator() => new int[0].GetEnumerator();
        }
    }
}