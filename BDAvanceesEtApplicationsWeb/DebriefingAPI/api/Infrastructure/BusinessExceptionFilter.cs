using System;
using System.Net;
using DDDDemo.DTO;
using DDDDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDDDemo.api.Infrastructure
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public BusinessExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("DDDDemo.api.Exceptions");
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Une erreur inattendue s'est produite");


            if (context.Exception.GetType()==typeof(DbUpdateConcurrencyException))
            {
               
                var result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    //Content = Newtonsoft.Json.JsonConvert.SerializeObject(new BusinessError() { Message = context.Exception.Message }),
                    ContentType = "application/json"

                };
                context.Result = result;
            }else
            if (context.Exception.GetType().IsSubclassOf(typeof(BusinessException)))
            {
                var result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = Newtonsoft.Json.JsonConvert.SerializeObject(new BusinessError() { Message = context.Exception.Message }),
                    ContentType = "application/json"

                };
                context.Result = result;
            }
        }
    }
}
