using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace XtramileWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet("weather-city")]
        public async Task<ActionResult> Index(string city)
        {
            return Ok();
        }
    }
}
