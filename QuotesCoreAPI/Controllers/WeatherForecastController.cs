using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuotesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesAPIController : ControllerBase
    {

        private readonly ILogger<QuotesAPIController> _logger;

        public QuotesAPIController(ILogger<QuotesAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var rng = new Random();
            return new JsonResult("test");
        }
    }
}
