using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeHelper.Models
{
    class DailyUptimeModel
    {
        public DateTime? Date { get; set; }
        public TimeSpan? Uptime { get; set; }
    }
}
