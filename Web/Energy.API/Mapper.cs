using Energy.API.Models;
using Energy.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Energy.API
{
    public class Mapper
    {
        public static EnergyData Map(IEnergyData data)
        {
            return new EnergyData
            {
                Timestamp = data.Timestamp,
                Tariff = data.Tariff,
                Consumed = data.Consumed,
                ConsumedRate1 = data.ConsumedRate1,
                ConsumedRate2 = data.ConsumedRate2,
                ReturnedRate1 = data.ReturnedRate1,
                ReturnedRate2 = data.ReturnedRate2,
                Gas = data.Gas,
                Returned = data.Returned
            };
        }

        public static IEnumerable<EnergyData> Map(IEnumerable<IEnergyData> data)
        {
            return data.Select(Map);
        }
    }
}