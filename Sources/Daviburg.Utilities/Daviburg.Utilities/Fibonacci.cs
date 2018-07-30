// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fibonacci.cs" company="Microsoft">
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

    public class Fibonacci
    {
        private static readonly Lazy<double> lazyFiveSquareRoot = new Lazy<double>(() => Math.Sqrt(5));
        private static readonly Lazy<double> lazyVarphi = new Lazy<double>(() => (1 + Fibonacci.lazyFiveSquareRoot.Value) / 2);
        private static readonly Lazy<double> lazyPsi = new Lazy<double>(() => (1 - Fibonacci.lazyFiveSquareRoot.Value) / 2);

        private static double FiveSquareRoot => lazyFiveSquareRoot.Value;
        private static double Varphi => lazyVarphi.Value;
        private static double Psi => lazyPsi.Value;

        public static uint Compute(uint n)
        {
            return Convert.ToUInt32(Math.Round((Math.Pow(Fibonacci.Varphi, n) - Math.Pow(Fibonacci.Psi, n)) / Fibonacci.FiveSquareRoot));
        }

        public static uint ClosestInputForOutcome(uint outcome)
        {
            return Convert.ToUInt32(Math.Floor(Math.Log(Fibonacci.FiveSquareRoot * outcome) / Math.Log(Fibonacci.Varphi)));
        }

        public static uint SummationOfEvenFibonacci(uint n)
        {
            var target = n / 3;
            uint sum = 0;
            for (uint index = 1; index <= target; index++)
            {
                sum += Fibonacci.Compute(index * 3);
            }

            return sum;
        }
    }
}
