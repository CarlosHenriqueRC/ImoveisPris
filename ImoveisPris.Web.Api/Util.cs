using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImoveisPris.Web.Api
{
    public class Util
    {

        public class HttpResponseExceptionFilter : IExceptionFilter
        {
            public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
            {
                //chech if this is divide by zero exception
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = 500
                };

                //set "handled" to true since exception is already property handled
                //and there is no need to run other filters
                context.ExceptionHandled = true;
            }

        }

        public class HttpResponseException : Exception
        {
            public int Status { get; set; } = 500;

            public object Value
            {
                get;
                set;
            }
        }
    }
}
