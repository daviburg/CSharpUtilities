// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtensions2Tests.cs" company="Microsoft">
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
    public class IntExtensions2Tests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void DigitsSummationPowerOfTwoTests()
        {
            var OeisA001370 = new[] { 1, 2, 4, 8, 7, 5, 10, 11, 13, 8, 7, 14, 19, 20, 22, 26, 25, 14, 19, 29, 31, 26, 25, 41, 37, 29, 40, 35, 43, 41, 37, 47, 58, 62, 61, 59, 64, 56, 67, 71, 61, 50, 46, 56, 58, 62, 70, 68, 73, 65, 76, 80, 79, 77, 82, 92, 85, 80, 70, 77 };
            for (var exponent = 0; exponent < OeisA001370.Length; exponent++)
            {
                Assert.AreEqual(OeisA001370[exponent], exponent.DigitsSummationPowerOfTwo());
            }

            Console.WriteLine($"The sum of digits of the number 2 power 1000 is {1000.DigitsSummationPowerOfTwo()}.");
        }
    }
}
