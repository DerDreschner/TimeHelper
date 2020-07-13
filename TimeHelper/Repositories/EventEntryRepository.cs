
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TimeHelper.Constants;
using TimeHelper.Models;

namespace TimeHelper.Repositories
{
    class EventEntryRepository
    {
        public static List<EventEntryModel> GetUptimeEntries(DateTime? startDate)
        {
            var systemLog = new EventLog
            {
                Log = "System"
            };

            var entries = systemLog.Entries.Cast<EventLogEntry>();

            var entrie = entries.Where(x => x.TimeGenerated >= startDate).Where(x => x.InstanceId == EventType.BOOTUP || x.InstanceId == EventType.SHUTDOWN || x.InstanceId == EventType.CRASH).ToList();

            var convertedEntries = new List<EventEntryModel>();

            entrie.ForEach(x => convertedEntries.Add(x));

            convertedEntries = convertedEntries.OrderBy(x => x.TimeGenerated).ToList();

            if (convertedEntries.First()?.EventId != EventType.BOOTUP) {
                var missingEntry = entries.Where(x => x.TimeGenerated < startDate).Where(x => x.InstanceId == EventType.BOOTUP).OrderByDescending(x => x.TimeGenerated).First();

                convertedEntries.Add(missingEntry);

                convertedEntries = convertedEntries.OrderBy(x => x.TimeGenerated).ToList();
            }

            return convertedEntries;
        }

    }
}
