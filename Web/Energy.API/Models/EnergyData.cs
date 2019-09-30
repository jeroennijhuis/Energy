using Energy.Core.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Energy.API.Models
{
    public class EnergyData
    {
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public double ConsumedRate1 { get; set; }
        [Required]
        public double ConsumedRate2 { get; set; }
        [Required]
        public double ReturnedRate1 { get; set; }
        [Required]
        public double ReturnedRate2 { get; set; }
        [Required]
        public Tariff Tariff { get; set; }
        [Required]
        public double Consumed { get; set; }
        [Required]
        public double Returned { get; set; }
        [Required]
        public double Gas { get; set; }
    }
}