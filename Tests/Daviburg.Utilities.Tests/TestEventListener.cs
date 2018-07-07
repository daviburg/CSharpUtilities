// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEventListener.cs" company="Microsoft">
//   This file is part of Daviburg Utilities.
//
//   Daviburg Utilities is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Daviburg Utilities is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Foobar.  If not, see<https://www.gnu.org/licenses/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Daviburg.Utilities.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Tracing;
    using System.Linq;

    /// <summary>
    /// A basic event listener helper class that records all event data for added event source(s)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class TestEventListener : EventListener
    {
        private static readonly Lazy<TestEventListener> EventListenerInstance = new Lazy<TestEventListener>(() => new TestEventListener());

        public static TestEventListener Log => EventListenerInstance.Value;

        private TestEventListener()
        {
        }

        private readonly List<EventWrittenEventArgs> receivedEvents = new List<EventWrittenEventArgs>();
        public IReadOnlyList<EventWrittenEventArgs> ReceivedEvents => this.receivedEvents as IReadOnlyList<EventWrittenEventArgs>;

        public bool AddEventSource(Guid sourceGuid)
        {
            var matchedEventSource = EventSource.GetSources().SingleOrDefault(eventSource => eventSource.Guid.Equals(sourceGuid));
            if (matchedEventSource == default(EventSource))
            {
                return false;
            }

            this.EnableEvents(matchedEventSource, EventLevel.LogAlways);
            return true;
        }

        public void Disable()
        {
            this.DisableEvents(new EventSource(string.Empty));
        }

        /// <summary>
        /// Helper to format the event payload
        /// </summary>
        public static string FormatPayload(ReadOnlyCollection<object> payload)
        {
            return payload == null || !payload.Any() ? null : string.Join(" ", payload.Select(obj => $"[{obj}]"));
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (this.receivedEvents.Count < 10000)
            {
                this.receivedEvents.Add(eventData);
            }
        }
    }
}
