// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Composite.cs" company="Microsoft">
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

namespace Daviburg.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Composite
    {
        private int? countofDivisors;
        private long? value;

        /// <summary>
        /// Instantiate a composite number from its value
        /// </summary>
        /// <param name="value">The composite number value</param>
        /// <remarks>
        /// To avoid performance overhead, this constructor does not validate that the value is not prime.
        /// A prime value input would not result in a valid composite number class instance.
        /// </remarks>
        public Composite(long value)
        {
            this.PrimeFactors = value.BlendPrimeFactorization();
        }

        public Composite(IReadOnlyList<PrimeFactor> primeFactors)
        {
            this.PrimeFactors = primeFactors;
        }

        public IReadOnlyList<PrimeFactor> PrimeFactors { get;  private set; }

        public int CountOfDivisors => this.countofDivisors ??
            (this.countofDivisors = this.PrimeFactors.Aggregate(seed: 1, func: (partialCount, primeFactor) => partialCount * (primeFactor.Exponent + 1))).Value;

        public long Value => this.value ??
            (this.value = this.PrimeFactors.Aggregate(
                seed: new Tuple<int, long>(0, 1),
                func: (indexAndPartialProduct, primeFactor) => new Tuple<int, long>(indexAndPartialProduct.Item1 + 1, indexAndPartialProduct.Item2 * primeFactor.Value),
                resultSelector: finalIndexAndProduct => finalIndexAndProduct.Item2))
            .Value;

    }
}
