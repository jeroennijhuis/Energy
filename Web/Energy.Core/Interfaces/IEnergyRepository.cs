using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Energy.Core.Interfaces
{
    public interface IEnergyRepository
    {
        Task<IEnumerable<IEnergyData>> Get(Guid device, int maxItems, DateTime? from, DateTime? to);
    }
}
