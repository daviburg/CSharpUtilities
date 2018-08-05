// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InlineTests.cs" company="Microsoft">
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

    /// <summary>
    /// Tests for code embedded in the test class and not part of the utilities.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InlineTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void SearchGreatestProduct()
        {
            var stringOfInt = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

            // Used sbyte to represent single digits as the smallest integer type available in C#
            var shortList = stringOfInt.Select(character => (sbyte)char.GetNumericValue(character));

            Assert.AreEqual(81, GreatestProduct(shortList, 2));
            Assert.AreEqual(5832, GreatestProduct(shortList, 4));
            GreatestProduct(shortList, 13);
        }

        /// <summary>
        /// Searches the greatest product from n adjacent digits in a series
        /// </summary>
        /// <param name="seriesOfDigits">The series of digits.</param>
        /// <param name="countOfAdjacentDigits">The count of adjacent digits to compute the product.</param>
        private static long GreatestProduct(IEnumerable<sbyte> seriesOfDigits, int countOfAdjacentDigits)
        {
            long greatestProduct = 0;
            var greatestProductFactors = new List<sbyte>();
            long runningProduct = 1;
            var sequentialDigits = new List<sbyte>();
            foreach (var digit in seriesOfDigits)
            {
                // Any zero will zero the product hence we can restart from an empty sequence after it
                if (digit == 0)
                {
                    sequentialDigits = new List<sbyte>();
                    runningProduct = 1;
                    continue;
                }

                sequentialDigits.Add(digit);
                runningProduct *= digit;

                // Test if we are at the desired count of adjacent digits
                if (sequentialDigits.Count == countOfAdjacentDigits)
                {
                    // Capture new max product if any
                    if (runningProduct > greatestProduct)
                    {
                        greatestProduct = runningProduct;
                        greatestProductFactors = new List<sbyte>(sequentialDigits);
                    }

                    // To avoid recomputing the whole sequence, remove just the first/oldest digit
                    runningProduct /= sequentialDigits[0];
                    sequentialDigits.RemoveAt(0);
                }
            }

            Console.WriteLine($"The greatest product is {greatestProduct} from multiplying the {countOfAdjacentDigits} digits {String.Join(", ", greatestProductFactors.Select<sbyte, string>(digit => digit.ToString()))}.");
            return greatestProduct;
        }
    }
}
