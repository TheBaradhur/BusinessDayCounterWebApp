using BusinessDayCounterWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BusinessDayCounterWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekDayCounterController : ControllerBase
    {
        private readonly IDateCounter _businessDayCounter;

        public WeekDayCounterController(IDateCounter businessDayCounter)
        {
            _businessDayCounter = businessDayCounter;
        }
        
        [HttpGet]
        public ActionResult<int> WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            return _businessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);
        }
    }
}