// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PythagoreanTriple.cs" company="Microsoft">
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

    /// <summary>
    /// Pythagorean triple, i.e. three positive integers a, b, and c, such that a^2 + b^2 = c^2.
    /// </summary>
    /// <remarks>See also <see href="https://en.wikipedia.org/wiki/Pythagorean_triple"/>.</remarks>
    public class PythagoreanTriple
    {
        #region private cached values

        private int integerA;
        private int integerB;
        private int integerC;

        #endregion

        /// <summary>
        /// Constructs a primitive Pythagorean triple from a pair of integer for the Euclid formula
        /// </summary>
        /// <param name="euclidM">Integer M.</param>
        /// <param name="euclidN">Integer N.</param>
        public PythagoreanTriple(int euclidM, int euclidN)
            : this(euclidM, euclidN, 1)
        {
        }

        /// <summary>
        /// Constructs a Pythagorean triple from a pair of integer for the Euclid formula
        /// </summary>
        /// <param name="euclidM">Integer M.</param>
        /// <param name="euclidN">Integer N.</param>
        /// <param name="factorK">Factor K, 1 for primitive triples.</param>
        public PythagoreanTriple(int euclidM, int euclidN, int factorK)
        {
            if (factorK < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(factorK), factorK, "Factor k for Euclid formula must be strictly greater than zero.");
            }

            if (euclidN < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(euclidN), euclidN, "Value N for Euclid formula must be strictly greater than zero.");
            }

            if (euclidM <= euclidN)
            {
                throw new ArgumentOutOfRangeException(nameof(euclidM), euclidM, "Value M for Euclid formula must be strictly greater than value N.");
            }

            this.EuclidM = euclidM;
            this.EuclidN = euclidN;
            this.FactorK = factorK;
        }

        public int EuclidM { get; private set; }
        public int EuclidN { get; private set; }
        public int FactorK { get; private set; }

        /// <summary>
        /// A = k * (m^2 - n^2)
        /// </summary>
        public int IntegerA { get { return integerA != 0 ? integerA : integerA = this.FactorK * (this.EuclidM * this.EuclidM - this.EuclidN * this.EuclidN); } }

        /// <summary>
        /// B = k * (2 * m * n)
        /// </summary>
        public int IntegerB { get { return integerB != 0 ? integerB : integerB = this.FactorK * (2 * this.EuclidM * this.EuclidN); } }

        /// <summary>
        /// C = k * (m^2 + n^2)
        /// </summary>
        public int IntegerC { get { return integerC != 0 ? integerC : integerC = this.FactorK * (this.EuclidM * this.EuclidM + this.EuclidN * this.EuclidN); } }
    }
}
