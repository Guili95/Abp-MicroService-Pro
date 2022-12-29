<<<<<<< HEAD
﻿using Guili.Localization;
using Guili.Shared.Hosting.Microservices.WrapResult.Attributes;
using Guili.Shared.Hosting.Microservices.WrapResult.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using Volo.Abp.Validation;

namespace Guili.Shared.Hosting.Microservices.WrapResult
{
    public sealed class ResultExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (ShouldHandleException(context))
            {
                return;
            }

            await HandleAndWrapException(context);
        }

        private bool ShouldHandleException(ExceptionContext context)
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

        private async Task HandleAndWrapException(ExceptionContext context)
        {
            var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var logLevel = context.Exception.GetLogLevel();

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<ResultExceptionFilter>>(NullLogger<ResultExceptionFilter>.Instance);

            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

            logger.LogException(context.Exception, logLevel);

            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));
            var result = SimplifyMessage(context, remoteServiceErrorInfo);
            context.Result = new ObjectResult(result);
            context.Exception = null; //Handled!
        }
        private AjaxResponse SimplifyMessage(ExceptionContext context, RemoteServiceErrorInfo remoteServiceErrorInfo)
        {
            AjaxResponse result = null;
            var localizer = context.GetService<IStringLocalizer<GuiliResource>>();
            bool unAuthorizedRequest = context.Exception is AbpAuthorizationException;
            switch (context.Exception)
            {
                case AbpAuthorizationException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Authentication:401"], null, "401"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 401;
                    break;
                // TODO: 这里用400是否适合?
                case AbpValidationException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(remoteServiceErrorInfo.Details, null, "400"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 400;
                    break;
                case EntityNotFoundException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Entity:506"], null, "506"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 506;
                    break;
                case NotImplementedException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Implemented:507"], null, "507"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 507;
                    break;
                case IBusinessException:
                    result = new AjaxResponse(
                        new RemoteServiceErrorInfo(remoteServiceErrorInfo.Message, null, remoteServiceErrorInfo.Code),
                        unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 508;
                    break;
                default:
                    {
                        result = new AjaxResponse(
                            new RemoteServiceErrorInfo(context.Exception.Message, null, "-1"),
                            unAuthorizedRequest
                        );
                        context.HttpContext.Response.StatusCode = 500;
                        break;
                    }
            }

            return result;
        }
    }
}
=======
﻿using Guili.Localization;
using Guili.Shared.Hosting.Microservices.WrapResult.Attributes;
using Guili.Shared.Hosting.Microservices.WrapResult.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using Volo.Abp.Validation;

namespace Guili.Shared.Hosting.Microservices.WrapResult
{
    public sealed class ResultExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (ShouldHandleException(context))
            {
                return;
            }

            await HandleAndWrapException(context);
        }

        private bool ShouldHandleException(ExceptionContext context)
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

        private async Task HandleAndWrapException(ExceptionContext context)
        {
            var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            var remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var logLevel = context.Exception.GetLogLevel();

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<ResultExceptionFilter>>(NullLogger<ResultExceptionFilter>.Instance);

            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

            logger.LogException(context.Exception, logLevel);

            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));
            var result = SimplifyMessage(context, remoteServiceErrorInfo);
            context.Result = new ObjectResult(result);
            context.Exception = null; //Handled!
        }
        private AjaxResponse SimplifyMessage(ExceptionContext context, RemoteServiceErrorInfo remoteServiceErrorInfo)
        {
            AjaxResponse result = null;
            var localizer = context.GetService<IStringLocalizer<GuiliResource>>();
            bool unAuthorizedRequest = context.Exception is AbpAuthorizationException;
            switch (context.Exception)
            {
                case AbpAuthorizationException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Authentication:401"], null, "401"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 401;
                    break;
                // TODO: 这里用400是否适合?
                case AbpValidationException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(remoteServiceErrorInfo.Details, null, "400"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 400;
                    break;
                case EntityNotFoundException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Entity:506"], null, "506"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 506;
                    break;
                case NotImplementedException:
                    result = new AjaxResponse(
                         new RemoteServiceErrorInfo(localizer["Implemented:507"], null, "507"),
                         unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 507;
                    break;
                case IBusinessException:
                    result = new AjaxResponse(
                        new RemoteServiceErrorInfo(remoteServiceErrorInfo.Message, null, remoteServiceErrorInfo.Code),
                        unAuthorizedRequest
                    );
                    context.HttpContext.Response.StatusCode = 508;
                    break;
                default:
                    {
                        result = new AjaxResponse(
                            new RemoteServiceErrorInfo(context.Exception.Message, null, "-1"),
                            unAuthorizedRequest
                        );
                        context.HttpContext.Response.StatusCode = 500;
                        break;
                    }
            }

            return result;
        }
    }
}
>>>>>>> git/ids4
