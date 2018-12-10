using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicAspNetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Get()
        {
            var eventId = new EventId(1, "Id");

            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                _logger.LogError(eventId, e, "Message Detail => {0} || {1}", new { OrderId = 2, Status = "Processing" }, new { CorrelationId = 123123123 });
            }

            return Ok();
        }
    }
}