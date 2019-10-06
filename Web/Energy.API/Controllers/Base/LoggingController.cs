using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Energy.API.Controllers.Base
{
    public abstract class LoggingController<T> : Controller
        where T : LoggingController<T>
    {
        protected readonly ILogger<T> Logger;

        protected LoggingController(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}