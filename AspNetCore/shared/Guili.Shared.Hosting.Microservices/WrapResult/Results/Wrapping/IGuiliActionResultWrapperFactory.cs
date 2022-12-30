using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.DependencyInjection;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping
{
    /// <summary>
    /// 结果包装工厂接口
    /// </summary>
    public interface IGuiliActionResultWrapperFactory : ITransientDependency
    {
        IGuiliActionResultWrapper CreateFor(FilterContext context);
    }
}
