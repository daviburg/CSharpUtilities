// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathAlgorithmsTests.cs" company="Microsoft">
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
    public class MathAlgorithmsTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void GreatestCommonDivisorTests()
        {
            Assert.AreEqual(0, MathAlgorithms.GreatestCommonDivisor(0, 0));
            Assert.AreEqual(123, MathAlgorithms.GreatestCommonDivisor(0, 123));
            Assert.AreEqual(123, MathAlgorithms.GreatestCommonDivisor(123, 0));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(3, 7));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(3, 7));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(7, 3));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(6, 35));
            Assert.AreEqual(1, MathAlgorithms.GreatestCommonDivisor(35, 6));
            Assert.AreEqual(12, MathAlgorithms.GreatestCommonDivisor(24, 36));
            Assert.AreEqual(12, MathAlgorithms.GreatestCommonDivisor(36, 24));
            Assert.AreEqual(21, MathAlgorithms.GreatestCommonDivisor(1071, 462));
            Assert.AreEqual(21, MathAlgorithms.GreatestCommonDivisor(462, 1071));
            Assert.AreEqual(151, MathAlgorithms.GreatestCommonDivisor(163231, 152057));
            Assert.AreEqual(151, MathAlgorithms.GreatestCommonDivisor(163231, 135749));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PowerModuloTests()
        {
            Assert.AreEqual(1, MathAlgorithms.PowerModulo(100, 0, 10));
            Assert.AreEqual(0, MathAlgorithms.PowerModulo(4, 6, 4));
            Assert.AreEqual(4, MathAlgorithms.PowerModulo(4, 6, 6));
            Assert.AreEqual(8, MathAlgorithms.PowerModulo(5, 7, 13));
        }
    }
}
