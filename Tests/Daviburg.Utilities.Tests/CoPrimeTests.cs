// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoPrimeTests.cs" company="Microsoft">
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
    public class CoPrimeTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void OddOddCoPrimesTest()
        {
            var oddOddCoPrimes = CoPrime.OddOddRootVertex.NextTriplet;
            Assert.AreEqual(5, oddOddCoPrimes[0].IntegerA);
            Assert.AreEqual(3, oddOddCoPrimes[0].IntegerB);
            Assert.AreEqual(7, oddOddCoPrimes[1].IntegerA);
            Assert.AreEqual(3, oddOddCoPrimes[1].IntegerB);
            Assert.AreEqual(5, oddOddCoPrimes[2].IntegerA);
            Assert.AreEqual(1, oddOddCoPrimes[2].IntegerB);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void EvenOddCoPrimesTest()
        {
            var oddOddCoPrimes = CoPrime.EvenOddRootVertex.NextTriplet;
            Assert.AreEqual(3, oddOddCoPrimes[0].IntegerA);
            Assert.AreEqual(2, oddOddCoPrimes[0].IntegerB);
            Assert.AreEqual(5, oddOddCoPrimes[1].IntegerA);
            Assert.AreEqual(2, oddOddCoPrimes[1].IntegerB);
            Assert.AreEqual(4, oddOddCoPrimes[2].IntegerA);
            Assert.AreEqual(1, oddOddCoPrimes[2].IntegerB);

            var rank2OddOddCoPrimes = oddOddCoPrimes[0].NextTriplet;
            Assert.AreEqual(4, rank2OddOddCoPrimes[0].IntegerA);
            Assert.AreEqual(3, rank2OddOddCoPrimes[0].IntegerB);
            Assert.AreEqual(8, rank2OddOddCoPrimes[1].IntegerA);
            Assert.AreEqual(3, rank2OddOddCoPrimes[1].IntegerB);
            Assert.AreEqual(7, rank2OddOddCoPrimes[2].IntegerA);
            Assert.AreEqual(2, rank2OddOddCoPrimes[2].IntegerB);

            rank2OddOddCoPrimes = oddOddCoPrimes[1].NextTriplet;
            Assert.AreEqual(8, rank2OddOddCoPrimes[0].IntegerA);
            Assert.AreEqual(5, rank2OddOddCoPrimes[0].IntegerB);
            Assert.AreEqual(12, rank2OddOddCoPrimes[1].IntegerA);
            Assert.AreEqual(5, rank2OddOddCoPrimes[1].IntegerB);
            Assert.AreEqual(9, rank2OddOddCoPrimes[2].IntegerA);
            Assert.AreEqual(2, rank2OddOddCoPrimes[2].IntegerB);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SearchPythagoreanTripleTest()
        {
            var tripleSummingToThousand = SearchPythagoreanTriple(1000);
            Console.WriteLine($"The PythagoreanTriple for 1000 is A: {tripleSummingToThousand.IntegerA}, B: {tripleSummingToThousand.IntegerB}, C: {tripleSummingToThousand.IntegerC} and of product {tripleSummingToThousand.IntegerA * tripleSummingToThousand.IntegerB * tripleSummingToThousand.IntegerC}, generated from M: {tripleSummingToThousand.EuclidM}, N: {tripleSummingToThousand.EuclidN} and factor K: {tripleSummingToThousand.FactorK}.");
        }

        /// <summary>
        /// Finds a Pythagorean triple which sum equals to the goal.
        /// </summary>
        /// <param name="sumGoal">Goal sum.</param>
        private PythagoreanTriple SearchPythagoreanTriple(int sumGoal)
        {
            var evenOddCoPrimesToTest = new List<CoPrime>() { CoPrime.EvenOddRootVertex };

            do
            {
                var testingCoPrime = evenOddCoPrimesToTest[0];
                evenOddCoPrimesToTest.RemoveAt(0);
                var magicNumber = 2 * testingCoPrime.IntegerA * testingCoPrime.IntegerA + 2 * testingCoPrime.IntegerA * testingCoPrime.IntegerB;
                if (magicNumber == sumGoal)
                {
                    return new PythagoreanTriple(testingCoPrime.IntegerA, testingCoPrime.IntegerB);
                }

                if (magicNumber < sumGoal)
                {
                    if (sumGoal % magicNumber == 0)
                    {
                        var factor = sumGoal / magicNumber;
                        return new PythagoreanTriple(testingCoPrime.IntegerA, testingCoPrime.IntegerB, factor);
                    }

                    evenOddCoPrimesToTest.AddRange(testingCoPrime.NextTriplet);
                }
            }
            while (true);
        }
    }
}
