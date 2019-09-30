using Energy.API.Controllers.Base;
using Energy.API.Models;
using Energy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Energy.API.Controllers
{
    public class DataController : EnergyController
    {
        public DataController(IEnergyRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Get energy data from a specific device.
        /// </summary>
        [HttpPost]
        [Route("device/{deviceid}/energy")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<EnergyData>))]
        public async Task<IActionResult> GetAsync([FromRoute] Guid deviceid, [FromQuery] int maxItems = 1000, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            var data = (await EnergyRepository.Get(deviceid, maxItems, from, to)).ToArray();
            return data.Any()
                ? (IActionResult)Ok(Mapper.Map(data))
                : NoContent();
        }
    }
}
