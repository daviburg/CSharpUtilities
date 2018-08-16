// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighCompositesTests.cs" company="Microsoft">
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
    public class HighCompositesTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void HighCompositesTest()
        {
            long[] highComposites = { 1, 2, 4, 6, 12, 24, 36, 48, 60, 120, 180, 240, 360, 720, 840, 1260, 1680, 2520, 5040, 7560, 10080, 15120, 20160, 25200, 27720, 45360, 50400, 55440, 83160, 110880, 166320, 221760, 277200, 332640, 498960, 554400, 665280, 720720 };
            long[] divisorsCounts = { 1, 2, 3, 4, 6, 8, 9, 10, 12, 16, 18, 20, 24, 30, 32, 36, 40, 48, 60, 64, 72, 80, 84, 90, 96, 100, 108, 120, 128, 144, 160, 168, 180, 192, 200, 216, 224, 240 };

            // Find 5 but let the remaining be discovered on-demand
            HighComposites.Singleton.FindHcns(5);

            for (int index = 0; index < highComposites.Length; index++)
            {
                Assert.AreEqual(highComposites[index], HighComposites.Singleton[index].Value);
                Assert.AreEqual(divisorsCounts[index], HighComposites.Singleton[index].CountOfDivisors);
            }
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void LargeHighCompositeTest()
        {
            // Famous numbers: 6746328388800 last on the list from Ramanujan (1915).
            // 4497552259200 at index 102 because Ramanujan didn't include 1 in his list (likely intentionally).
            // 293318625600 which he missed but this algorithm doesn't.
            Assert.AreEqual((long)293318625600, HighComposites.Singleton[90].Value);
            Assert.AreEqual((long)4497552259200, HighComposites.Singleton[102].Value);
            Assert.AreEqual((long)6746328388800, HighComposites.Singleton[103].Value);
            for (var index = 0; index < 164; index++)
            {
                Console.WriteLine($"Order {index + 1:D3} has {HighComposites.Singleton[index].CountOfDivisors:D5} divisors for value {HighComposites.Singleton[index].Value}.");
            }

            Assert.AreEqual((long)4488062423933088000, HighComposites.Singleton[163].Value);
            Assert.AreEqual(138240, HighComposites.Singleton[163].CountOfDivisors);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void TriangularCompositeTest()
        {
            // First search for the lowest HCN that has more than 500 divisors.
            // Any number less that than, triangular or not, will have no more than 500 divisors, by definition of HCN.
            var index = 0;
            while (HighComposites.Singleton[index].CountOfDivisors <= 500)
            {
                index++;
            }

            Console.WriteLine($"First HCN with over 500 divisors is {HighComposites.Singleton[index].Value} with {HighComposites.Singleton[index].CountOfDivisors} divisors.");

            var triangular = Triangular.ClosestTriangularNumber(HighComposites.Singleton[index].Value);

            Console.WriteLine($"Jump-starting the search at triangular number of order {triangular.Order} and value {triangular.Value}.");

            while (triangular.Value.CountOfDivisors() < 500)
            {
                triangular = triangular.NextTriangularNumber();
            }

            Console.WriteLine($"The secret number is the triangular number of order {triangular.Order} and value {triangular.Value}.");
        }
    }
}
