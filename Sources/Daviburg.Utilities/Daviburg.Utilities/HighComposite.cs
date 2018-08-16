// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighComposite.cs" company="Microsoft">
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

    public partial class HighComposites
    {
        /// <summary>
        /// A high composite number initialized from the ordered list of exponents of its prime factorization form.
        /// </summary>
        /// <remarks>This class is only meant to be instantiated by containing class <see cref="HighComposites"/>.</remarks>
        public class HighComposite : IEquatable<HighComposite>, IComparable<HighComposite>
        {
            private int? countofDivisors;
            private long? value;

            internal HighComposite(IReadOnlyList<int> exponents) => this.Exponents = exponents;

            public readonly IReadOnlyList<int> Exponents;

            /// <summary>
            /// d(n) aka the count of divisors is the product of all exponents each increased by one, exponents of the prime factorization of n.
            /// </summary>
            public int CountOfDivisors => this.countofDivisors ?? (this.countofDivisors = this.Exponents.Aggregate(1, (partialCount, exponent) => partialCount * (exponent + 1))).Value;

            public long Value => this.value ?? 
                (this.value = this.Exponents.Aggregate(
                    seed: new Tuple<int, long>(0, 1), 
                    func: (indexAndPartialProduct, exponent) => new Tuple<int, long>(indexAndPartialProduct.Item1 + 1, indexAndPartialProduct.Item2 * Primes.Singleton[indexAndPartialProduct.Item1].Power(exponent)), 
                    resultSelector: finalIndexAndProduct => finalIndexAndProduct.Item2))
                .Value;

            /// <summary>
            /// Tries computing value, returns true if overflows long during the value computation.
            /// </summary>
            public bool Overflows
            {
                get
                {
                    var overflows = false;
                    try 
                    {
                        checked
                        {
                            this.value = this.Exponents.Aggregate(
                                seed: new Tuple<int, long>(0, 1),
                                func: (indexAndPartialProduct, exponent) => new Tuple<int, long>(indexAndPartialProduct.Item1 + 1, indexAndPartialProduct.Item2 * Primes.Singleton[indexAndPartialProduct.Item1].Power(exponent)),
                                resultSelector: finalIndexAndProduct => finalIndexAndProduct.Item2);
                        }
                    }
                    catch (OverflowException)
                    {
                        overflows = true;
                    }

                    return overflows;
                }
            }

            public override bool Equals(Object otherObject)
            {
                return otherObject is HighComposite && this == (HighComposite)otherObject;
            }

            public override int GetHashCode()
            {
                // Comparing the exponents is tempting but value is computed once then all comparisons are O(1)
                // otherwise the list of exponents is traversed again and again as a list gets sorted.
                return this.Value.GetHashCode() ^ this.Value.GetHashCode();
            }

            public bool Equals(HighComposite other)
            {
                return this == other;
            }

            public int CompareTo(HighComposite other)
            {
                // A null value means that this object is greater.
                return (other == null) ? 1 : this.Value.CompareTo(other.Value);
            }

            public static bool operator ==(HighComposite leftHcn, HighComposite rightHcn)
            {
                return leftHcn?.Value == rightHcn?.Value;
            }

            public static bool operator !=(HighComposite leftHcn, HighComposite rightHcn)
            {
                return !(leftHcn == rightHcn);
            }
        }
    }
}
