// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Power.cs" company="Microsoft">
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
    /// A integer base number raised by an integer exponent with JIT value computation, and custom comparison.
    /// </summary>
    public class IntegerPower
    {
        private long? value;

        /// <summary>
        /// Instantiate a <see cref="Power"/>
        /// </summary>
        /// <param name="baseNumber">The base.</param>
        /// <param name="exponent">The exponent</param>
        /// <remarks>Base been a reserved word in C# the argument has the verbose name baseNumber.</remarks>
        public IntegerPower(long baseNumber, int exponent)
        {
            this.Base = baseNumber;
            this.Exponent = exponent;
        }

        public long Base { get; private set; }

        public int Exponent { get; private set; }

        public long Value => (value ?? (value = this.Base.Power(this.Exponent))).Value;

        public override bool Equals(Object otherObject)
        {
            return otherObject is IntegerPower && this == (IntegerPower)otherObject;
        }

        public bool Equals(IntegerPower other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            // NOTE(daviburg): It is unnecessary to compare the actual values as they are determined by the base and exponent.
            return this.Base.GetHashCode() ^ this.Exponent.GetHashCode();
        }

        public static bool operator ==(IntegerPower leftPower, IntegerPower rightPower)
        {
            return leftPower?.Base == rightPower?.Base && leftPower?.Exponent == rightPower?.Exponent;
        }

        public static bool operator !=(IntegerPower leftPower, IntegerPower rightPower)
        {
            return !(leftPower == rightPower);
        }
    }
}
