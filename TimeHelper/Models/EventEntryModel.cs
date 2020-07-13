using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Constants;
using TimeHelper.Extensions;

namespace TimeHelper.Models {
    class EventEntryModel {
        public Int64 EventId { get; set; }
        public DateTime TimeGenerated { get; set; }

        public static implicit operator EventEntryModel(EventLogEntry eventLogEntry) {
            return new EventEntryModel {
                EventId = eventLogEntry.InstanceId,
                TimeGenerated = eventLogEntry.InstanceId == EventType.CRASH ? eventLogEntry.GetCrashTime() : eventLogEntry.TimeGenerated
            };
        }
    }
}
