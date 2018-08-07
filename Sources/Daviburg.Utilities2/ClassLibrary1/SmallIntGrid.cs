// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmallIntGrid.cs" company="Microsoft">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SmallIntGrid
    {
        private sbyte[,] grid;

        public SmallIntGrid(string gridInString)
        {
            var rows = gridInString.Split('\n');
            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var rowInString = rows[rowIndex];
                var integers = rowInString.Split(' ').Select(valueAsString => sbyte.Parse(valueAsString)).ToList();
                if (this.grid == null)
                {
                    this.grid = new sbyte[integers.Count(), rows.Count()];
                }

                for (int columnIndex = 0; columnIndex < integers.Count; columnIndex++)
                {
                    this.grid[rowIndex, columnIndex] = integers[columnIndex];
                }
            }
        }

        public long this[int rowIndex, int columnIndex]
        {
            get => this.grid[rowIndex, columnIndex];
        }

        public sbyte[] GetRow(int rowIndex)
        {
            var row = new sbyte[this.ColumnCount];
            Buffer.BlockCopy(
                src: this.grid, 
                srcOffset: rowIndex * this.ColumnCount * sizeof(sbyte),
                dst: row, 
                dstOffset: 0,
                count: this.ColumnCount * sizeof(sbyte));
            return row;
        }

        public sbyte[] GetColumn(int columnIndex)
        {
            return Enumerable.Range(0, this.RowCount - 1)
                .Select(rowIndex => this.grid[rowIndex, columnIndex])
                .ToArray();
        }

        public int ColumnCount => this.grid.GetLength(0);
        public int RowCount => this.grid.GetLength(1);
        public int DiagonalHalfCount => this.ColumnCount + this.RowCount - 1;

        /// <summary>
        /// Gets the Nth diagonal progressing right and up the grid
        /// </summary>
        /// <param name="diagonalIndex">The diagonal index.</param>
        /// <remarks>
        /// There are column count + row count - 1 diagonal in the grid.
        /// Diagonal zero is the upper right value of the grid, the last diagonal is the lower left value of the grid.
        /// </remarks>
        public sbyte[] GetRightDownDiagonal(int diagonalIndex)
        {
            var diagonalLength = diagonalIndex < this.RowCount ?
                Math.Min(diagonalIndex + 1, this.ColumnCount) :
                Math.Min(this.ColumnCount + this.RowCount - (diagonalIndex + 1), this.RowCount);
            return diagonalIndex < this.RowCount ?
                Enumerable
                    .Range(0, diagonalLength)
                    .Select(position => this.grid[position, position + this.RowCount - (diagonalIndex + 1)])
                    .ToArray() :
                Enumerable
                    .Range(0, diagonalLength)
                    .Select(position => this.grid[position + (diagonalIndex - this.RowCount), position + diagonalIndex + 1 - this.RowCount])
                    .ToArray();
        }

        /// <summary>
        /// Gets the Nth diagonal progressing right and up the grid
        /// </summary>
        /// <param name="diagonalIndex">The diagonal index.</param>
        /// <remarks>
        /// There are column count + row count - 1 diagonal in the grid.
        /// Diagonal zero is the upper left value of the grid, the last diagonal is the lower right value of the grid.
        /// </remarks>
        public sbyte[] GetRightUpDiagonal(int diagonalIndex)
        {
            var diagonalLength = diagonalIndex < this.RowCount ?
                Math.Min(diagonalIndex + 1, this.ColumnCount) :
                Math.Min(this.ColumnCount + this.RowCount - (diagonalIndex + 1), this.RowCount);
            return diagonalIndex < this.RowCount ?
                Enumerable
                    .Range(0, diagonalLength)
                    .Select(position => this.grid[diagonalIndex - position, position])
                    .ToArray() :
                Enumerable
                    .Range(0, diagonalLength)
                    .Select(position => this.grid[this.RowCount - (position + 1), position + diagonalIndex + 1 - this.RowCount])
                    .ToArray();
        }

        public long GreatestProductOfAdjacentNumbers(int countOfAdjacentNumbers)
        {
            var arraysOfNumbers = new List<sbyte[]>();
            for (var columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
            {
                arraysOfNumbers.Add(this.GetColumn(columnIndex));
            }

            for (var rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
            {
                arraysOfNumbers.Add(this.GetRow(rowIndex));
            }

            for (var diagonalIndex = 0; diagonalIndex < this.DiagonalHalfCount; diagonalIndex++)
            {
                arraysOfNumbers.Add(this.GetRightDownDiagonal(diagonalIndex));
                arraysOfNumbers.Add(this.GetRightUpDiagonal(diagonalIndex));
            }

            return arraysOfNumbers.Max(array => new SmallIntSeries(array).GreatestProduct(countOfAdjacentNumbers));
        }
    }
}
