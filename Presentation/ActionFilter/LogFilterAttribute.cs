using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
        }

        private string Log(string modelName, RouteData routeDate)
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelName,
                Controller = routeDate.Values["controller"],
                Action = routeDate.Values["action"]
            };

            if(routeDate.Values.Count >=3)
                logDetails.Id = routeDate.Values["Id"];

            return logDetails.ToString();
        }
    }
}
