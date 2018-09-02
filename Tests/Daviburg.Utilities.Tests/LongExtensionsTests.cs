// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LongExtensionsTests.cs" company="Microsoft">
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
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LongExtensionsTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void IsPrimeTests()
        {
            checked
            { 
                var primeIndex = 0;

                // Tested overnight up to primeIndex 3837026 for Primes.Singleton.LargestDiscoveredPrime 64936643
                for (long testValue = 0; testValue < 200000; testValue++)
                {
                    if (testValue != Primes.Singleton[primeIndex])
                    {
                        Assert.IsFalse(testValue.IsPrime(), $"{testValue} is not prime.");
                    }
                    else
                    {
                        Assert.IsTrue(testValue.IsPrime(), $"{testValue} is prime.");
                        primeIndex++;
                    }
                }

                Assert.IsFalse(((long)8780191).IsPrime(), $"{8780191} is not prime.");
                Assert.IsTrue(((long)8780207).IsPrime(), $"{8780207} is prime.");
                Assert.IsTrue(((long)10780223).IsPrime(), $"{10780223} is prime.");
                Assert.IsTrue(((long)2147483647).IsPrime(), $"{2147483647} is prime.");
                Assert.IsTrue(((long)2147483659).IsPrime(), $"{2147483659} is prime.");

                // BUGBUG: Should be prime per https://numbermatics.com/n/4294967291/
                ////Assert.IsTrue(((long)4294967291).IsPrime(), $"{4294967291} is prime.");
                // BUGBUG: Should be prime per https://numbermatics.com/n/4294967279/
                ////Assert.IsTrue(((long)4294967279).IsPrime(), $"{4294967279} is prime.");
                // BUGBUG: Should be prime per https://numbermatics.com/n/4447483681/
                //// Assert.IsTrue(4447483681.IsPrime(), $"{4447483681} is prime.");
                // BUGBUG: Should be prime per https://numbermatics.com/n/4447483687/
                //// Assert.IsTrue(4447483687.IsPrime(), $"{4447483687} is prime.");
                Assert.IsFalse(4447483683.IsPrime(), $"{4447483683} is not prime.");
            }
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void RhoPrimeFactorizationTests()
        {
            CollectionAssert.AreEquivalent(expected: new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1) }, actual: ((long)2).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 2, exponent: 1) }, ((long)3).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2) }, ((long)4).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 3, exponent: 1) }, ((long)5).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1), new PrimeFactor(2, 1) }, ((long)6).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 1) }, ((long)12).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 5), new PrimeFactor(2, 3) }, ((long)864).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 2), new PrimeFactor(3, 2) }, ((long)900).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 7, exponent: 1),
                    new PrimeFactor(baseNumber: 41, exponent: 1),
                    new PrimeFactor(baseNumber: 30593, exponent: 1)
                }, 
                ((long)8780191).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 2, exponent: 4),
                    new PrimeFactor(baseNumber: 3, exponent: 2),
                    new PrimeFactor(baseNumber: 5, exponent: 1),
                    new PrimeFactor(baseNumber: 7, exponent: 1),
                    new PrimeFactor(baseNumber: 11, exponent: 1),
                    new PrimeFactor(baseNumber: 13, exponent: 1)
                },
                ((long)720720).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 2, exponent: 1),
                    new PrimeFactor(baseNumber: 3, exponent: 1),
                    new PrimeFactor(baseNumber: 5, exponent: 1),
                    new PrimeFactor(baseNumber: 89, exponent: 1),
                    new PrimeFactor(baseNumber: 191, exponent: 1),
                    new PrimeFactor(baseNumber: 4211, exponent: 1)
                },
                ((long)2147483670).RhoPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 1409, exponent: 1),
                    new PrimeFactor(baseNumber: 1524119, exponent: 1)
                },
                ((long)2147483671).RhoPrimeFactorization());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void BlendPrimeFactorizationTests()
        {
            CollectionAssert.AreEquivalent(expected: new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1) }, actual: ((long)2).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 2, exponent: 1) }, ((long)3).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2) }, ((long)4).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 3, exponent: 1) }, ((long)5).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 1), new PrimeFactor(2, 1) }, ((long)6).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 1) }, ((long)12).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 5), new PrimeFactor(2, 3) }, ((long)864).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(new List<PrimeFactor> { new PrimeFactor(order: 1, exponent: 2), new PrimeFactor(2, 2), new PrimeFactor(3, 2) }, ((long)900).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 7, exponent: 1),
                    new PrimeFactor(baseNumber: 41, exponent: 1),
                    new PrimeFactor(baseNumber: 30593, exponent: 1)
                },
                ((long)8780191).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 2, exponent: 4),
                    new PrimeFactor(baseNumber: 3, exponent: 2),
                    new PrimeFactor(baseNumber: 5, exponent: 1),
                    new PrimeFactor(baseNumber: 7, exponent: 1),
                    new PrimeFactor(baseNumber: 11, exponent: 1),
                    new PrimeFactor(baseNumber: 13, exponent: 1)
                },
                ((long)720720).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 2, exponent: 1),
                    new PrimeFactor(baseNumber: 3, exponent: 1),
                    new PrimeFactor(baseNumber: 5, exponent: 1),
                    new PrimeFactor(baseNumber: 89, exponent: 1),
                    new PrimeFactor(baseNumber: 191, exponent: 1),
                    new PrimeFactor(baseNumber: 4211, exponent: 1)
                },
                ((long)2147483670).BlendPrimeFactorization());
            CollectionAssert.AreEquivalent(
                new List<PrimeFactor>
                {
                    new PrimeFactor(baseNumber: 1409, exponent: 1),
                    new PrimeFactor(baseNumber: 1524119, exponent: 1)
                },
                ((long)2147483671).BlendPrimeFactorization());
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void ProperDivisorsTests()
        {
            var properDivisorsList = new List<List<long>>
            {
                new List<long> { 1 },
                new List<long> { 1 },
                new List<long> { 1, 2 },
                new List<long> { 1 },
                new List<long> { 1, 2, 3 },
                new List<long> { 1 },
                new List<long> { 1, 2, 4 },
                new List<long> { 1, 3 },
                new List<long> { 1, 2, 5 },
                new List<long> { 1 },
                new List<long> { 1, 2, 3, 4, 6 },
                new List<long> { 1 },
                new List<long> { 1, 2, 7 },
                new List<long> { 1, 3, 5 },
                new List<long> { 1, 2, 4, 8 },
                new List<long> { 1 },
                new List<long> { 1, 2, 3, 6, 9 },
                new List<long> { 1 },
                new List<long> { 1, 2, 4, 5, 10 },
                new List<long> { 1, 3, 7 },
                new List<long> { 1, 2, 11 },
                new List<long> { 1 },
                new List<long> { 1, 2, 3, 4, 6, 8, 12 },
                new List<long> { 1, 5 },
                new List<long> { 1, 2, 13 },
                new List<long> { 1, 3, 9 },
                new List<long> { 1, 2, 4, 7, 14 },
                new List<long> { 1 },
                new List<long> { 1, 2, 3, 5, 6, 10, 15 },
            };

            for (var index = 0; index < properDivisorsList.Count; index++)
            {
                CollectionAssert.AreEquivalent(expected: properDivisorsList[index], actual: ((long)index + 2).ProperDivisors().ToList());
            }
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SumOfProperDivisorsTests()
        {
            var sumOfproperDivisorsList = new List<long>
            {
                1, 1, 3, 1, 6, 1, 7, 4, 8, 1, 16, 1, 10, 9, 15, 1, 21, 1, 22, 11, 14, 1, 36, 6, 16, 13, 28, 1, 42, 1, 31, 15, 20, 13, 55, 1, 22, 17, 50, 1, 54, 1, 40
            };

            for (var index = 0; index < sumOfproperDivisorsList.Count; index++)
            {
                Assert.AreEqual(expected: sumOfproperDivisorsList[index], actual: ((long)index + 2).SumOfProperDivisors());
            }
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void AmicableTests()
        {
            var arrayKnownSumOfDivisors = new long[10001];
            var amicableList = new List<int>();
            for (var index = 10000; index > 2; index-=2)
            {
                arrayKnownSumOfDivisors[index] = ((long)index).SumOfProperDivisors();
                if (arrayKnownSumOfDivisors[index] > index && arrayKnownSumOfDivisors[index] < 10000)
                {
                    if (arrayKnownSumOfDivisors[(int)arrayKnownSumOfDivisors[index]] == index)
                    {
                        amicableList.AddRange(new[] { index, (int)arrayKnownSumOfDivisors[index] });
                    }
                }
            }

            Console.WriteLine($"The sum of all the amicable numbers under 10000 is {amicableList.Aggregate((sumSoFar, amicableNumber) => sumSoFar + amicableNumber)}.");
        }
    }
}
