using System;
using System.Collections.Generic;
using TimeHelper.Constants;
using TimeHelper.Models;
using System.Linq;

namespace TimeHelper.Converter {

    class EventEntryListToUptimeEntryListConverter {

        public static List<UptimeEntryModel> Converter(List<EventEntryModel> entries) {

            DateTime? lastBoot = null;
            DateTime? lastShutdown = null;
            bool isCrashed = false;
            var lastEntry = entries.Last();
            List<UptimeEntryModel> events = new List<UptimeEntryModel>();

            foreach (var eventInstance in entries) {

                switch (eventInstance.EventId) {
                    case EventType.BOOTUP:
                        lastBoot = eventInstance.TimeGenerated;
                        break;

                    case EventType.SHUTDOWN:
                        lastShutdown = eventInstance.TimeGenerated;
                        isCrashed = false;
                        break;

                    case EventType.CRASH:
                        lastShutdown = eventInstance.TimeGenerated;
                        isCrashed = true;
                        break;
                }

                if (IsValidEntry(lastBoot, lastShutdown)) {

                    events.Add(new UptimeEntryModel {
                        BootupTime = lastBoot,
                        ShutdownTime = lastShutdown,
                        IsCrashed = isCrashed
                    });

                    lastBoot = null;
                    lastShutdown = null;
                    isCrashed = false;
                }

                if (IsLastEntry(eventInstance, lastEntry) && IsCurrentUptime(lastBoot, lastShutdown)) {
                    events.Add(new UptimeEntryModel {
                        BootupTime = lastBoot
                    });
                }
            }

            return events;
        }

        private static bool IsValidEntry(DateTime? lastBootupTime, DateTime? lastShutdownTime) {
            return lastBootupTime.HasValue && lastShutdownTime.HasValue;
        }

        private static bool IsCurrentUptime(DateTime? lastBootupTime, DateTime? lastShutdownTime) {
            return lastBootupTime.HasValue && !lastShutdownTime.HasValue;
        }

        private static bool IsLastEntry(EventEntryModel currentEntry, EventEntryModel lastEntry) {
            return currentEntry.Equals(lastEntry);
        }

    }
    
}
