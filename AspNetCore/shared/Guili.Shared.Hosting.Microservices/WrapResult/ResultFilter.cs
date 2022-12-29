<<<<<<< HEAD
﻿using Guili.Shared.Hosting.Microservices.WrapResult.Attributes;
using Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;

namespace Guili.Shared.Hosting.Microservices.WrapResult
{
    public class ResultFilter : IAsyncResultFilter, ITransientDependency
    {
        private readonly IGuiliActionResultWrapperFactory _actionResultWrapperFactory;

        public ResultFilter(
            IGuiliActionResultWrapperFactory actionResultWrapper)
        {
            _actionResultWrapperFactory = actionResultWrapper;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.ActionDescriptor.IsControllerAction())
            {
                if (!ShouldHandleException(context))
                {
                    var methodInfo = context.ActionDescriptor.GetMethodInfo();
                    var wrapResultAttribute =
                        ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                            methodInfo,
                            new WrapResultAttribute()
                        );
                    if (wrapResultAttribute.WrapOnSuccess)
                    {
                        _actionResultWrapperFactory.CreateFor(context).Wrap(context);
                    }
                }
            }
            await next.Invoke();
        }

        private bool ShouldHandleException(ResultExecutingContext context)
        {
            if (context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
            {
                return true;
            }

            if (context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
            {
                return true;
            }

            return false;
        }
    }
}
=======
﻿using Guili.Shared.Hosting.Microservices.WrapResult.Attributes;
using Guili.Shared.Hosting.Microservices.WrapResult.Results.Wrapping;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;

namespace Guili.Shared.Hosting.Microservices.WrapResult
{
    public class ResultFilter : IAsyncResultFilter, ITransientDependency
    {
        private readonly IGuiliActionResultWrapperFactory _actionResultWrapperFactory;

        public ResultFilter(
            IGuiliActionResultWrapperFactory actionResultWrapper)
        {
            _actionResultWrapperFactory = actionResultWrapper;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.ActionDescriptor.IsControllerAction())
            {
                if (!ShouldHandleException(context))
                {
                    var methodInfo = context.ActionDescriptor.GetMethodInfo();
                    var wrapResultAttribute =
                        ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                            methodInfo,
                            new WrapResultAttribute()
                        );
                    if (wrapResultAttribute.WrapOnSuccess)
                    {
                        _actionResultWrapperFactory.CreateFor(context).Wrap(context);
                    }
                }
            }
            await next.Invoke();
        }

        private bool ShouldHandleException(ResultExecutingContext context)
        {
            if (context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
            {
                return true;
            }

            if (context.ActionDescriptor.GetMethodInfo().GetCustomAttributes(typeof(DontWrapResultAttribute), true).Any())
            {
                return true;
            }

            return false;
        }
    }
}
>>>>>>> git/ids4
