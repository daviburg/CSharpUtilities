// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtensionsTests.cs" company="Microsoft">
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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IntExtensionsTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void IsPalindromeSimpleTests()
        {
            for (int testValue = 0; testValue < 10; testValue++)
            {
                Assert.IsTrue(testValue.IsPalindrome());
            }

            Assert.IsTrue(11.IsPalindrome());
            Assert.IsTrue(101.IsPalindrome());
            Assert.IsTrue(1001.IsPalindrome());
            Assert.IsTrue(21012.IsPalindrome());
            Assert.IsTrue(210012.IsPalindrome());
            Assert.IsTrue(3210123.IsPalindrome());
            Assert.IsTrue(32100123.IsPalindrome());

            Assert.IsFalse(10.IsPalindrome());
            Assert.IsFalse(101001.IsPalindrome());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LargestPalindromeFinderTests()
        {
            // Looking for a specific large palindrome.
            // Guessing the palindrome will close to 999 square, xyzzyx where x will then be 9,
            // for the multiplication to end with 9 we need the lower digits to be one of the three pairs
            // 1 and 9, or 3 and 3, or 7 and 7.
            // The higher digits are guessed to be 9 as resulting in higher product.
            // This could be further optimized with the knowledge that the palindrome must be a factor of a certain
            // prime number, thus the pairs of quantities must include one of 913, 957 or 979.
            // Some have found one last optimization will reduce the number of multiplications from 30 to 3.
            var candidates = new List<int>();
            for (int leftMiddleDigit = 0; leftMiddleDigit < 10; leftMiddleDigit++)
            {
                for (int rightMiddleDigit = 0; rightMiddleDigit < 10; rightMiddleDigit++)
                {
                    foreach (var candidate in new[]
                        {
                            (909 + (leftMiddleDigit * 10)) * (901 + (rightMiddleDigit * 10)),
                            (903 + (leftMiddleDigit * 10)) * (903 + (rightMiddleDigit * 10)),
                            (907 + (leftMiddleDigit * 10)) * (907 + (rightMiddleDigit * 10))
                        })
                    {
                        if (candidate.IsPalindrome())
                        {
                            candidates.Add(candidate);
                        }
                    }
                }
            }

            candidates.Sort();
            //// Assert.AreEqual(*sanitized*, candidates[0]);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SummationSimpleTests()
        {
            Assert.AreEqual(0, 0.Summation());
            Assert.AreEqual(1, 1.Summation());
            Assert.AreEqual(3, 2.Summation());
            Assert.AreEqual(6, 3.Summation());
            Assert.AreEqual(10, 4.Summation());
            Assert.AreEqual(15, 5.Summation());
            Assert.AreEqual(21, 6.Summation());
            Assert.AreEqual(55, 10.Summation());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SquareSummationSimpleTests()
        {
            Assert.AreEqual(0, 0.SquareSummation());
            Assert.AreEqual(1, 1.SquareSummation());
            Assert.AreEqual(5, 2.SquareSummation());
            Assert.AreEqual(14, 3.SquareSummation());
            Assert.AreEqual(30, 4.SquareSummation());
            Assert.AreEqual(55, 5.SquareSummation());
            Assert.AreEqual(91, 6.SquareSummation());
            Assert.AreEqual(140, 7.SquareSummation());
            Assert.AreEqual(385, 10.SquareSummation());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void DifferenceOfSummationTests()
        {
            var squareOfSummationOf10 = Convert.ToInt64(Math.Pow(10.Summation(), 2));
            Assert.AreEqual(3025, squareOfSummationOf10);
            Assert.AreEqual(2640, Convert.ToInt64(Math.Pow(10.Summation(), 2)) - 10.SquareSummation());
            //// Assert.AreEqual(*sanitized*, *sanitized*.SquareSummation() - *sanitized*.Summation());
            Assert.AreEqual(23, 3 * 3.Summation() + 5 * 1.Summation() - 15 * 0.Summation());
            //// Assert.AreEqual(*sanitized *, 3 * (999 / 3).Summation() + 5 * (999 / 5).Summation() - 15 * (999 / 15).Summation());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PrimeFactorizationTests()
        {
            CollectionAssert.AreEqual(expected: new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1) }, actual: 2.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 2, exponent: 1) }, 3.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2) }, 4.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 3, exponent: 1) }, 5.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1), new PrimeFactor(2, 1) }, 6.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 1) }, 12.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 5), new PrimeFactor(2, 3) }, 864.PrimeFactorization());
            CollectionAssert.AreEqual(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 2), new PrimeFactor(3, 2) }, 900.PrimeFactorization());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void CollatzSimpleTests()
        {
            Assert.AreEqual(4, 1.CollatzSequenceNext());
            Assert.AreEqual(1, 2.CollatzSequenceNext());
            Assert.AreEqual(10, 3.CollatzSequenceNext());
            Assert.AreEqual(2, 4.CollatzSequenceNext());
            Assert.AreEqual(16, 5.CollatzSequenceNext());
            Assert.AreEqual(3, 6.CollatzSequenceNext());
            Assert.AreEqual(22, 7.CollatzSequenceNext());
            Assert.AreEqual(4, 8.CollatzSequenceNext());
            Assert.AreEqual(28, 9.CollatzSequenceNext());
            Assert.AreEqual(5, 10.CollatzSequenceNext());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void FactorialTests()
        {
            // Checked factorial computation versus cached table.
            for (var value = 0; value <= 20; value++)
            {
                Assert.AreEqual(Factorials.Singleton[value], value.Factorial(), $"Factorial of {value} differs between cached table {Factorials.Singleton[value]} and computed value {value.Factorial()}.");
            }
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void FractionDecimalPeriodLengthTests()
        {
            var a051626 = new[] { 0, 0, 1, 0, 0, 1, 6, 0, 1, 0, 2, 1, 6, 6, 1, 0, 16, 1, 18, 0, 6, 2, 22, 1, 0, 6, 3, 6, 28, 1, 15, 0, 2, 16, 6, 1, 3, 18, 6, 0, 5, 6, 21, 2, 1, 22, 46, 1, 42, 0, 16, 6, 13, 3, 2, 6, 18, 28, 58, 1, 60, 15, 6, 0, 6, 2, 33, 16, 22, 6, 35, 1, 8, 3, 1, 18, 6, 6, 13, 0, 9, 5, 41, 6, 16, 21, 28, 2, 44, 1 };

            for (var index = 0; index < a051626.Length; index++)
            {
                Assert.AreEqual(expected: a051626[index], actual: (index + 1).FractionDecimalPeriodLength());
            }
        }
    }
}
