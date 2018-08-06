// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PythagoreanTripleTests.cs" company="Microsoft">
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
    public class PythagoreanTripleTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void PythagoreanTripleSimpleTests()
        {
            var simplestTriple = new PythagoreanTriple(2, 1);
            Assert.AreEqual(3, simplestTriple.IntegerA);
            Assert.AreEqual(4, simplestTriple.IntegerB);
            Assert.AreEqual(5, simplestTriple.IntegerC);
        }
    }
}
