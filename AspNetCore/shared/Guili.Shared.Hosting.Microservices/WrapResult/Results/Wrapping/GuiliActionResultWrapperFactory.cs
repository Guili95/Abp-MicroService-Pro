using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    public class GuiliActionResultWrapperFactory : IGuiliActionResultWrapperFactory
    {
        public IGuiliActionResultWrapper CreateFor(FilterContext context)
        {
            Check.NotNull(context, nameof(context));

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is ObjectResult:
                    return new GuiliObjectActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is JsonResult:
                    return new GuiliJsonActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is EmptyResult:
                    return new GuiliEmptyActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is ObjectResult:
                    return new GuiliObjectActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is JsonResult:
                    return new GuiliJsonActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is EmptyResult:
                    return new GuiliEmptyActionResultWrapper();

                default:
                    return new GuiliNullActionResultWrapper();
            }
        }
    }
}
