using Energy.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Energy.API.Controllers.Base
{
    [ApiController]
    public abstract class EnergyController : Controller
    {
        protected readonly IEnergyRepository EnergyRepository;

        protected EnergyController(IEnergyRepository repository)
        {
            EnergyRepository = repository;
        }
    }
}
