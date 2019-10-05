using Energy.API.Converters;
using Energy.Core.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Energy.API.Models
{
    /// <summary>
    /// One record of energy data
    /// </summary>
    public class EnergyData
    {
        /// <summary>
        /// Timestamp of data retrieved
        /// </summary>
        /// <example>2019-10-05 20:15:30</example>
        [Required]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Total consumption (*kWh) at a low tariff
        /// </summary>
        /// <example>9906.692</example>
        [Required]
        public double ConsumedRate1 { get; set; }

        /// <summary>
        /// Total consumption (*kWh) at a high tariff
        /// </summary>
        /// <example>9986.035</example>
        [Required]
        public double ConsumedRate2 { get; set; }

        /// <summary>
        /// Total returned (*kWh) at a low tariff
        /// </summary>
        /// <example>0.000</example>
        [Required]
        public double ReturnedRate1 { get; set; }

        /// <summary>
        /// Total returned (*kWh) at a high tariff
        /// </summary>
        /// <example>0.000</example>
        [Required]
        public double ReturnedRate2 { get; set; }

        /// <summary>
        /// Current Tariff (1:Low / 2:High)
        /// </summary>
        /// <example>Low</example>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Tariff Tariff { get; set; }

        /// <summary>
        /// Current consumption (*kW)
        /// </summary>
        /// <example>5.015</example>
        [Required]
        public double Consumed { get; set; }

        /// <summary>
        /// Current return (*kW)
        /// </summary>
        /// <example>0.000</example>
        [Required]
        public double Returned { get; set; }

        /// <summary>
        /// Total gas consumption (*m3)
        /// </summary>
        /// <example>4058.952</example>
        [Required]
        public double Gas { get; set; }
    }
}