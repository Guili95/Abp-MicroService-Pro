using Microsoft.AspNetCore.Mvc.Filters;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    public class GuiliNullActionResultWrapper : IGuiliActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {

        }
    }
}
