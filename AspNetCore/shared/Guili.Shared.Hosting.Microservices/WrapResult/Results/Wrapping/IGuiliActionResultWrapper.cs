using Microsoft.AspNetCore.Mvc.Filters;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    public interface IGuiliActionResultWrapper
    {
        void Wrap(FilterContext context);
    }
}
