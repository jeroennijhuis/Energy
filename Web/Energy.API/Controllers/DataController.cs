using Energy.API.Controllers.Base;
using Energy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Energy.API.Controllers
{
    public class DataController : EnergyController
    {
        public DataController(IEnergyRepository repository) : base(repository)
        {
        }

        [HttpGet("device/{guid}/energy")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid device, [FromQuery] int maxItems = 1000, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            var data = (await EnergyRepository.Get(device, maxItems, from, to)).ToArray();
            return data.Any()
                ? Ok(Mapper.Map(data))
                : (IActionResult)NoContent();
        }
    }
}
