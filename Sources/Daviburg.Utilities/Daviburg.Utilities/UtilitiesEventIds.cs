// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtilitiesEventIds.cs" company="Microsoft">
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
    /// <summary>
    /// Utilities ETW Event Source Event Ids
    /// </summary>
    /// <remarks>Each ETW Event method must have its unique id, used for generating the event source metadata.</remarks>
    public enum UtilitiesEventIds
    {
        ConcurrentLockDictionaryAcquired = 1,

        ConcurrentLockDictionaryReleased = 2,

        ConcurrentLockDictionaryRemoved = 3
    }
}