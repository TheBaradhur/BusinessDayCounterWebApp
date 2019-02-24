using BusinessDayCounterWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BusinessDayCounterWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekDayCounterController : ControllerBase
    {
        private readonly IDateCounter _businessDayCounter;

        private readonly ILogger _logger;

        public WeekDayCounterController(IDateCounter businessDayCounter, ILogger<WeekDayCounterController> logger)
        {
            _businessDayCounter = businessDayCounter;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<int> WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            _logger.LogTrace("WeekdaysBetweenTwoDates: incoming call with firstDate {0}, secondDate {1}.", firstDate, secondDate);

            var result = _businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            _logger.LogTrace("GetBusinessDaysBetweenTwoDates: result is {0}.", result);

            return result;
        }
    }
}