using Amazon.DynamoDBv2.DataModel;
using Energy.Core.Enumerations;
using Energy.Core.Interfaces;
using Energy.Repository.DynamoDb.Converters;
using System;

namespace Energy.Repository.DynamoDb.Models
{
    [DynamoDBTable("energy")]
    public class EnergyData : IEnergyData
    {
        [DynamoDBHashKey("deviceid")]
        public Guid DeviceId { get; set; }
        [DynamoDBRangeKey("timestamp", typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [DynamoDBProperty]
        public double ConsumedRate1 { get; set; }
        [DynamoDBProperty]
        public double ConsumedRate2 { get; set; }
        [DynamoDBProperty]
        public double ReturnedRate1 { get; set; }
        [DynamoDBProperty]
        public double ReturnedRate2 { get; set; }
        [DynamoDBProperty]
        public Tariff Tariff { get; set; }
        [DynamoDBProperty]
        public double Consumed { get; set; }
        [DynamoDBProperty]
        public double Returned { get; set; }
        [DynamoDBProperty]
        public double Gas { get; set; }
    }
}