using System;

namespace SampleServer
{
    public abstract class BackgroundServicesOptions
    {
    }

    public class BackgroundServiceOption
    {
        public TimeSpan RunFrom { get; set; } = TimeSpan.FromHours(0);
        public TimeSpan RunTo { get; set; } = TimeSpan.FromHours(24);
        public int Interval { get; set; }
    }
}