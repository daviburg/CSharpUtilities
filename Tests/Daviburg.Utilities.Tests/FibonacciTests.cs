// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FibonacciTests.cs" company="Microsoft">
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

namespace Daviburg.Utilities.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FibonacciTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void FibonacciSimpleTests()
        {
            Assert.AreEqual(expected: (uint)0, actual: Fibonacci.Compute(0));
            Assert.AreEqual(expected: (uint)1, actual: Fibonacci.Compute(1));
            Assert.AreEqual(expected: (uint)1, actual: Fibonacci.Compute(2));
            Assert.AreEqual(expected: (uint)2, actual: Fibonacci.Compute(3));
            Assert.AreEqual(expected: (uint)3, actual: Fibonacci.Compute(4));
            Assert.AreEqual(expected: (uint)5, actual: Fibonacci.Compute(5));
            Assert.AreEqual(expected: (uint)8, actual: Fibonacci.Compute(6));
            Assert.AreEqual(expected: (uint)13, actual: Fibonacci.Compute(7));
            Assert.AreEqual(expected: (uint)21, actual: Fibonacci.Compute(8));
            Assert.AreEqual(expected: (uint)34, actual: Fibonacci.Compute(9));
            Assert.AreEqual(expected: (uint)55, actual: Fibonacci.Compute(10));
            Assert.AreEqual(expected: (uint)89, actual: Fibonacci.Compute(11));
            Assert.AreEqual(expected: (uint)144, actual: Fibonacci.Compute(12));
            Assert.AreEqual(expected: (uint)233, actual: Fibonacci.Compute(13));
            Assert.AreEqual(expected: (uint)377, actual: Fibonacci.Compute(14));
            Assert.AreEqual(expected: (uint)610, actual: Fibonacci.Compute(15));
            Assert.AreEqual(expected: (uint)987, actual: Fibonacci.Compute(16));
            Assert.AreEqual(expected: (uint)1597, actual: Fibonacci.Compute(17));
            Assert.AreEqual(expected: (uint)2584, actual: Fibonacci.Compute(18));
            Assert.AreEqual(expected: (uint)4181, actual: Fibonacci.Compute(19));
            Assert.AreEqual(expected: (uint)6765, actual: Fibonacci.Compute(20));
            Assert.AreEqual(expected: (uint)10946, actual: Fibonacci.Compute(21));
            Assert.AreEqual(expected: (uint)17711, actual: Fibonacci.Compute(22));
            Assert.AreEqual(expected: (uint)28657, actual: Fibonacci.Compute(23));
            Assert.AreEqual(expected: (uint)46368, actual: Fibonacci.Compute(24));
            Assert.AreEqual(expected: (uint)75025, actual: Fibonacci.Compute(25));
            Assert.AreEqual(expected: (uint)121393, actual: Fibonacci.Compute(26));
            Assert.AreEqual(expected: (uint)196418, actual: Fibonacci.Compute(27));
            Assert.AreEqual(expected: (uint)317811, actual: Fibonacci.Compute(28));
            Assert.AreEqual(expected: (uint)514229, actual: Fibonacci.Compute(29));
            Assert.AreEqual(expected: (uint)832040, actual: Fibonacci.Compute(30));
            Assert.AreEqual(expected: (uint)1346269, actual: Fibonacci.Compute(31));
            Assert.AreEqual(expected: (uint)2178309, actual: Fibonacci.Compute(32));
            Assert.AreEqual(expected: (uint)3524578, actual: Fibonacci.Compute(33));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SummationOfEvenFibonacciSimpleTests()
        {
            Assert.AreEqual(expected: (uint)0, actual: Fibonacci.SummationOfEvenFibonacci(0));
            Assert.AreEqual(expected: (uint)0, actual: Fibonacci.SummationOfEvenFibonacci(1));
            Assert.AreEqual(expected: (uint)0, actual: Fibonacci.SummationOfEvenFibonacci(2));
            Assert.AreEqual(expected: (uint)2, actual: Fibonacci.SummationOfEvenFibonacci(3));
            Assert.AreEqual(expected: (uint)2, actual: Fibonacci.SummationOfEvenFibonacci(4));
            Assert.AreEqual(expected: (uint)2, actual: Fibonacci.SummationOfEvenFibonacci(5));
            Assert.AreEqual(expected: (uint)10, actual: Fibonacci.SummationOfEvenFibonacci(6));
            Assert.AreEqual(expected: (uint)44, actual: Fibonacci.SummationOfEvenFibonacci(9));
            Assert.AreEqual(expected: (uint)188, actual: Fibonacci.SummationOfEvenFibonacci(12));
            Assert.AreEqual(expected: (uint)798, actual: Fibonacci.SummationOfEvenFibonacci(15));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void InputSearchFibonacciTests()
        {
            Assert.AreEqual((uint)33, Fibonacci.ClosestInputForOutcome(4000000));

            // Next values are sanitized to not spoil some online challenge
            //// Assert.AreEqual(expected: (uint)*sanitized*, actual: Fibonacci.SumationOfEvenFibonacci(Fibonacci.ClosestInputForOutcome(4000000)));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SmallestNumberForNDigitsFibonacciTests()
        {
            // This is https://oeis.org/A072354 with a padding 0 in front
            var a072354Sequence = new uint[] { 0, 1, 7, 12, 17, 21, 26, 31, 36, 40, 45, 50, 55, 60, 64, 69, 74, 79, 84, 88, 93, 98, 103, 107, 112, 117, 122, 127, 131, 136, 141, 146, 151, 155, 160, 165, 170, 174, 179, 184, 189, 194, 198, 203, 208, 213, 217, 222, 227, 232, 237 };

            for (uint value = 1; value < a072354Sequence.Length; value++)
            {
                Assert.AreEqual(expected: a072354Sequence[value], actual: Fibonacci.SmallestNumberForNDigitsFibonacci(value));
            }

            Console.WriteLine($"The first Fibonacci number with 1000 digits is of order {Fibonacci.SmallestNumberForNDigitsFibonacci(1000)}");
        }
    }
}
