using System;

namespace FMG.NRT.Components.Alerts
{
    public class Alert
    {
        public String Message { get; set; }
        public AlertType Type { get; set; }
        public Int32 Timeout { get; set; }
        public String Id { get; set; }
    }
}
