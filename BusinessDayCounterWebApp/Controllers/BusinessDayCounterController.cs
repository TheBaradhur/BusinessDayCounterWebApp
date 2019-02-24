using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace BusinessDayCounterWebApp.Controllers
{
    [ApiController]
    [Route("api/business-day-counter")]
    public class BusinessDayCounterController : ControllerBase
    {
        private readonly IDateCounter _businessDayCounter;
        private readonly ILogger _logger;

        public BusinessDayCounterController(IDateCounter businessDayCounter, ILogger<BusinessDayCounterController> logger)
        {
            _businessDayCounter = businessDayCounter;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<int> GetBusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, List<DateTime> publicHolidays)
        {
            _logger.LogTrace("GetBusinessDaysBetweenTwoDates: incoming call with firstDate {0}, secondDate {1} and {2} holidays.", firstDate, secondDate, publicHolidays.Count);

            var result = _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            _logger.LogTrace("GetBusinessDaysBetweenTwoDates: result is {0}.", result);

            return result;
        }
        
        [HttpGet("custom-holidays")]
        public ActionResult<int> BusinessDaysBetweenTwoDatesCustomHolidays(DateTime firstDate, DateTime secondDate, List<PublicHoliday> customPublicHolidays)
        {
            _logger
                .LogTrace("BusinessDaysBetweenTwoDatesCustomHolidays: incoming call with firstDate {0}, secondDate {1} and {2} custom holidays.",
                firstDate,
                secondDate,
                customPublicHolidays.Count);

            var result =  _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, customPublicHolidays);
            
            _logger.LogTrace("GetBusinessDaysBetweenTwoDates: result is {0}.", result);

            return Ok(result);
        }
    }
}