// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmallIntSeries.cs" company="Microsoft">
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

namespace Daviburg.Utilities2
{
    // Enable if you use console output
    ////using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Series of small integers such as single and double digits integers.
    /// </summary>
    /// <remarks>Used sbyte to represent single digits as the smallest integer type available in C#.</remarks>
    public class SmallIntSeries
    {
        private IEnumerable<sbyte> seriesOfSmallIntegers;

        public SmallIntSeries(IEnumerable<sbyte> seriesOfDigits)
        {
            this.seriesOfSmallIntegers = seriesOfDigits;
        }

        public SmallIntSeries(string stringOfInt)
        {
            this.seriesOfSmallIntegers = stringOfInt.Select(character => (sbyte)char.GetNumericValue(character));
        }

        /// <summary>
        /// Searches the greatest product from n adjacent numbers in a series
        /// </summary>
        /// <param name="seriesOfDigits">The series of digits.</param>
        /// <param name="countOfAdjacentNumbers">The count of adjacent numbers to compute the product.</param>
        public long GreatestProduct(int countOfAdjacentNumbers)
        {
            long greatestProduct = 0;
            var greatestProductFactors = new List<sbyte>();
            long runningProduct = 1;
            var sequentialDigits = new List<sbyte>();
            foreach (var digit in this.seriesOfSmallIntegers)
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
                if (sequentialDigits.Count == countOfAdjacentNumbers)
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

            // Enable the following line if you'd like to see what numbers were used to get the greatest product.
            ////Console.WriteLine($"The greatest product is {greatestProduct} from multiplying the {countOfAdjacentNumbers} numbers {String.Join(", ", greatestProductFactors.Select<sbyte, string>(digit => digit.ToString()))}.");
            return greatestProduct;
        }
    }
}
