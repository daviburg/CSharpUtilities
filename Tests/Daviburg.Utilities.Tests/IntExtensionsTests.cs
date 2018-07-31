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
    }
}
