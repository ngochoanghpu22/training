using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Training.WebApi.Filters
{
    public class TimeTrackFilter : Attribute, IActionFilter
    {
        private Stopwatch _stopWatch;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var milliseconds = _stopWatch.ElapsedMilliseconds;
            var action = context.ActionDescriptor.DisplayName;
            Debug.WriteLine($"Action {action}, execute in {milliseconds} milliseconds");

            _stopWatch.Stop();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
    }
}
