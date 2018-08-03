// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrimesTests.cs" company="Microsoft">
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
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PrimesTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PrimesSimpleTests()
        {
            Assert.AreEqual(expected: 2, actual: Primes.Singleton[0]);
            Assert.AreEqual(expected: 3, actual: Primes.Singleton[1]);
            Assert.AreEqual(expected: 5, actual: Primes.Singleton[2]);
            Assert.AreEqual(expected: 7, actual: Primes.Singleton[3]);
            Assert.AreEqual(expected: 11, actual: Primes.Singleton[4]);
            Assert.AreEqual(expected: 13, actual: Primes.Singleton[5]);
            Assert.AreEqual(expected: 17, actual: Primes.Singleton[6]);
            Assert.AreEqual(expected: 19, actual: Primes.Singleton[7]);
            Assert.AreEqual(expected: 23, actual: Primes.Singleton[8]);
            //// Assert.AreEqual(expected: *sanitized *, actual: Primes.Singleton[10000]);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LargestPrimeFactorOfTests()
        {
            Assert.AreEqual(expected: 29, actual: Primes.Singleton.LargestPrimeFactorOf(13195));

            // Next values are sanitized to not spoil some online challenge
            //// Assert.AreEqual(expected: *sanitized*, actual: Primes.Singleton.LargestPrimeFactorOf(*sanitized*));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SmallestMultipleOfAllNumbersToTests()
        {
            Assert.AreEqual(expected: 2, actual: Primes.Singleton.SmallestMultipleOfAllNumbersTo(2));
            Assert.AreEqual(expected: 6, actual: Primes.Singleton.SmallestMultipleOfAllNumbersTo(3));
            Assert.AreEqual(expected: 12, actual: Primes.Singleton.SmallestMultipleOfAllNumbersTo(4));
            Assert.AreEqual(expected: 2520, actual: Primes.Singleton.SmallestMultipleOfAllNumbersTo(10));

            // Next values are sanitized to not spoil some online challenge
            //// Assert.AreEqual(expected: *sanitized*, actual: Primes.Singleton.SmallestMultipleOfAllNumbersTo(*sanitized*));
        }
    }
}
