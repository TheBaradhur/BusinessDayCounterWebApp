using BusinessDayCounterWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessDayCounterController : ControllerBase
    {
        private readonly IDateCounter _businessDayCounter;

        public BusinessDayCounterController(IDateCounter businessDayCounter)
        {
            _businessDayCounter = businessDayCounter;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<int> GetBusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHoliday)
        {
            return _businessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHoliday);
        }
    }
}