using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    public class GuiliJsonActionResultWrapper : IGuiliActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            JsonResult jsonResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    jsonResult = resultExecutingContext.Result as JsonResult;
                    break;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    jsonResult = pageHandlerExecutedContext.Result as JsonResult;
                    break;
            }

            if (jsonResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(jsonResult.Value is AjaxResponseBase))
            {
                jsonResult.Value = new AjaxResponse(jsonResult.Value);
            }
        }
    }
}
