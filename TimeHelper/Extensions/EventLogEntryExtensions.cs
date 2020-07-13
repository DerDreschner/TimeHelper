using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Constants;

namespace TimeHelper.Extensions {
    public static class EventLogEntryExtensions {
        public static DateTime GetCrashTime(this EventLogEntry logEntry) {
            var crashTime = $"{logEntry.ReplacementStrings[1]} {logEntry.ReplacementStrings[0]}".RemoveFormatCharacters();
            return DateTime.Parse(crashTime);
        }
    }
}
