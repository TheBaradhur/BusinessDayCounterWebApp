using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services;
using Microsoft.AspNetCore.Mvc;
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

        public BusinessDayCounterController(IDateCounter businessDayCounter)
        {
            _businessDayCounter = businessDayCounter;
        }
        
        [HttpGet]
        public ActionResult<int> GetBusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, List<DateTime> publicHoliday)
        {
            return _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHoliday);
        }
        
        [HttpGet("custom-holidays")]
        public ActionResult<int> BusinessDaysBetweenTwoDatesCustomHolidays(DateTime firstDate, DateTime secondDate, List<PublicHoliday> publicHolidays)
        {
            var result =  _businessDayCounter.BusinessDaysBetweenTwoDatesCustomHolidays(firstDate, secondDate, publicHolidays);

            if (result == -1)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}