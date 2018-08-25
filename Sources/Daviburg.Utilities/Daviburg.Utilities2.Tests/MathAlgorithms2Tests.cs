// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathAlgorithms2Tests.cs" company="Microsoft">
//   This file is part of Daviburg Utilities.
//
//   Daviburg Utilities is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Daviburg Utilities is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Daviburg Utilities.  If not, see <see href="https://www.gnu.org/licenses"/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities2.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MathAlgorithms2Tests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LongestCollatzSequenceStartingNumberTests()
        {
            Assert.AreEqual(new Tuple<int, int>(1, 1), MathAlgorithms2.LongestCollatzSequenceStartingNumber(2));
            Assert.AreEqual(new Tuple<int, int>(2, 2), MathAlgorithms2.LongestCollatzSequenceStartingNumber(3));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber(4));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber(5));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber(6));
            Assert.AreEqual(new Tuple<int, int>(6, 9), MathAlgorithms2.LongestCollatzSequenceStartingNumber(7));
            Assert.AreEqual(new Tuple<int, int>(7, 17), MathAlgorithms2.LongestCollatzSequenceStartingNumber(8));
            Assert.AreEqual(new Tuple<int, int>(7, 17), MathAlgorithms2.LongestCollatzSequenceStartingNumber(9));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber(10));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber(11));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber(12));

            // Test values from https://en.wikipedia.org/wiki/Collatz_conjecture#Examples
            // Note that Wikipedia measures the count of steps while we measure the sequence length such that our values are added one from theirs.
            Assert.AreEqual(new Tuple<int, int>(27, 112), MathAlgorithms2.LongestCollatzSequenceStartingNumber(28));
            Assert.AreEqual(new Tuple<int, int>(97, 119), MathAlgorithms2.LongestCollatzSequenceStartingNumber(100));
            Assert.AreEqual(new Tuple<int, int>(871, 179), MathAlgorithms2.LongestCollatzSequenceStartingNumber(1000));
            Assert.AreEqual(new Tuple<int, int>(6171, 262), MathAlgorithms2.LongestCollatzSequenceStartingNumber(10000));

            // Higher input value 100000 will fail because the sequence will go sufficiently high such that array allocation will fail. A sparsed array approach is required.
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LongestCollatzSequenceStartingNumber2Tests()
        {
            Assert.AreEqual(new Tuple<int, int>(1, 1), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(2));
            Assert.AreEqual(new Tuple<int, int>(2, 2), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(3));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(4));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(5));
            Assert.AreEqual(new Tuple<int, int>(3, 8), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(6));
            Assert.AreEqual(new Tuple<int, int>(6, 9), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(7));
            Assert.AreEqual(new Tuple<int, int>(7, 17), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(8));
            Assert.AreEqual(new Tuple<int, int>(7, 17), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(9));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(10));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(11));
            Assert.AreEqual(new Tuple<int, int>(9, 20), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(12));

            // Test values from https://en.wikipedia.org/wiki/Collatz_conjecture#Examples
            // Note that Wikipedia measures the count of steps while we measure the sequence length such that our values are added one from theirs.
            Assert.AreEqual(new Tuple<int, int>(27, 112), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(28));
            Assert.AreEqual(new Tuple<int, int>(97, 119), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(100));
            Assert.AreEqual(new Tuple<int, int>(871, 179), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(1000));
            Assert.AreEqual(new Tuple<int, int>(6171, 262), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(10000));
            Assert.AreEqual(new Tuple<int, int>(77031, 351), MathAlgorithms2.LongestCollatzSequenceStartingNumber2(100000));

            var theSecret = MathAlgorithms2.LongestCollatzSequenceStartingNumber2(1000000);
            Console.WriteLine($"The longest Collatz sequence with a starting number less than one million starts with {theSecret.Item1} with a lenght of {theSecret.Item2} elements.");
        }
    }
}
