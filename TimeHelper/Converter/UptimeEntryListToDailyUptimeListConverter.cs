using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeHelper.Models;

namespace TimeHelper.Converter {
    class UptimeEntryListToDailyUptimeListConverter {
        public static Dictionary<DateTime, TimeSpan> Convert(List<UptimeEntryModel> entries) {
            var convertedList = new Dictionary<DateTime, TimeSpan>();

            foreach (var entry in entries) {

                if (entry.IsCurrentSession || entry.BootupTime.Value.Date == entry.ShutdownTime.Value.Date) {
                    var date = entry.BootupTime.HasValue ? entry.BootupTime.Value.Date : entry.ShutdownTime.Value.Date;

                    if (convertedList.ContainsKey(date)) {
                        convertedList[date] += (TimeSpan)entry.Uptime;
                    } else {
                        convertedList.Add(date, (TimeSpan)entry.Uptime);
                    }
                } else {
                    CalculateHoursPerDay((DateTime)entry.BootupTime, (DateTime)entry.ShutdownTime, convertedList);
                }
            }

            return convertedList;
        }

        private static void CalculateHoursPerDay(DateTime start, DateTime finish, Dictionary<DateTime, TimeSpan> resultList) {
            if (start.Date == finish.Date) {
                var difference = finish.Date - start.Date;

                if (resultList.ContainsKey(start.Date)) {
                    resultList[start.Date] += difference;
                } else {
                    resultList.Add(start.Date, difference);
                }
            } else {
                var difference = start.Date.AddDays(1) - start;

                if (resultList.ContainsKey(start.Date)) {
                    resultList[start.Date] += difference;
                } else {
                    resultList.Add(start.Date, difference);
                }

                CalculateHoursPerDay(start.Date.AddDays(1), finish, resultList);
            }
        }
    }
}
