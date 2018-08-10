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
    /// A base number raised by an exponent with JIT value computation, and custom comparison.
    /// </summary>
    public class Power
    {
        private double? value;

        /// <summary>
        /// Instantiate a <see cref="Power"/>
        /// </summary>
        /// <param name="baseNumber">The base.</param>
        /// <param name="exponent">The exponent</param>
        /// <remarks>Base been a reserved word in C# the argument has the verbose name baseNumber.</remarks>
        public Power(double baseNumber, double exponent)
        {
            this.Base = baseNumber;
            this.Exponent = exponent;
        }

        public double Base { get;  private set; }

        public double Exponent { get; private set; }

        public double Value => (value ?? (value = Math.Pow(this.Base, this.Exponent))).Value;

        public override bool Equals(Object otherObject)
        {
            return otherObject is Power && this == (Power)otherObject;
        }

        public override int GetHashCode()
        {
            // NOTE(daviburg): It is unnecessary to compare the actual values as they are determined by the base and exponent.
            return this.Base.GetHashCode() ^ this.Exponent.GetHashCode();
        }

        public static bool operator ==(Power leftPower, Power rightPower)
        {
            return leftPower?.Base == rightPower?.Base && leftPower?.Exponent == rightPower?.Exponent;
        }

        public static bool operator !=(Power leftPower, Power rightPower)
        {
            return !(leftPower == rightPower);
        }
    }
}