using Energy.Core.Enumerations;
using System;

namespace Energy.Core.Interfaces
{
    public interface IEnergyData
    {
        Guid DeviceId { get; set; }
        DateTime Timestamp { get; set; }
        double ConsumedRate1 { get; set; }
        double ConsumedRate2 { get; set; }
        double ReturnedRate1 { get; set; }
        double ReturnedRate2 { get; set; }
        Tariff Tariff { get; set; }
        double Consumed { get; set; }
        double Returned { get; set; }
        double Gas { get; set; }
    }
}