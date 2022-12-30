using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    public class GuiliEmptyActionResultWrapper : IGuiliActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    resultExecutingContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    resultExecutingContext.Result = new ObjectResult(new AjaxResponse());
                    return;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    pageHandlerExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    pageHandlerExecutedContext.Result = new ObjectResult(new AjaxResponse());
                    return;
            }
        }
    }
}
