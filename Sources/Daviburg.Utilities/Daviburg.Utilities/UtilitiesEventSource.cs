// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtilitiesEventSource.cs" company="Microsoft">
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
    using System.Diagnostics.Tracing;

    /// <summary>
    /// ETW event tracing for utilities.
    /// </summary>
    [EventSource(Name = "Daviburg-Utilities")]
    public class UtilitiesEventSource : EventSource
    {
        private static readonly Lazy<UtilitiesEventSource> EventSourceInstance = new Lazy<UtilitiesEventSource>(() => new UtilitiesEventSource());

        public static UtilitiesEventSource Log => EventSourceInstance.Value;

        /// <summary>
        /// Prevents construction of instances by others, enforcing singleton pattern as intended by .NET team authors of EventSource
        /// </summary>
        private UtilitiesEventSource()
        {
        }

        #region Concurrent Lock Dictionary

        [Event(eventId: (int)UtilitiesEventIds.ConcurrentLockDictionaryAcquired, Level = EventLevel.Verbose)]
        public void ConcurrentLockDictionaryAcquired(string key, int count)
        {
            this.WriteEvent((int)UtilitiesEventIds.ConcurrentLockDictionaryAcquired, key, count);
        }

        [Event(eventId: (int)UtilitiesEventIds.ConcurrentLockDictionaryReleased, Level = EventLevel.Verbose)]
        public void ConcurrentLockDictionaryReleased(string key, int count)
        {
            this.WriteEvent((int)UtilitiesEventIds.ConcurrentLockDictionaryReleased, key, count);
        }

        [Event(eventId: (int)UtilitiesEventIds.ConcurrentLockDictionaryRemoved, Level = EventLevel.Verbose)]
        public void ConcurrentLockDictionaryRemoved(string key)
        {
            this.WriteEvent((int)UtilitiesEventIds.ConcurrentLockDictionaryRemoved, key);
        }

        #endregion
    }
}