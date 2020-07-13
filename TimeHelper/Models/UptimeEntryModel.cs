using System;
using System.Windows.Navigation;

namespace TimeHelper.Models {
    class UptimeEntryModel {
        private DateTime? _BootupTime { get; set; }
        public DateTime? BootupTime {
            get { return _BootupTime; }
            set {
                if (value > ShutdownTime) {
                    throw new ArgumentOutOfRangeException($"{nameof(BootupTime)} can't be newer then {nameof(ShutdownTime)}.");
                } else {
                    _BootupTime = value;
                }
            }
        }

        private DateTime? _ShutdownTime { get; set; }
        public DateTime? ShutdownTime {
            get { return _ShutdownTime; }
            set {
                if (value < BootupTime) {
                    throw new ArgumentOutOfRangeException($"{nameof(ShutdownTime)} can't be older then {nameof(BootupTime)}.");
                } else {
                    _ShutdownTime = value;
                }
            }
        }

        public TimeSpan Uptime {
            get {
                if (IsCurrentSession) {
                    return DateTime.Now - BootupTime.Value;
                } else {
                    return (TimeSpan)(ShutdownTime - BootupTime);
                }
            }
        }

        public bool IsCurrentSession {
            get { return (BootupTime.HasValue && !ShutdownTime.HasValue); }
        }

        public bool IsCrashed { get; set; }

    }
}
