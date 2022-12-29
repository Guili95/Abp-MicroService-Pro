<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Guili.Shared.Hosting.AspNetCore
{
    public class HiddenAbpDefaultApiFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    string key = "/" + apiDescription.RelativePath;
                    var reuslt = IsHidden(key, swaggerDoc.Info.Title);
                    if (reuslt) swaggerDoc.Paths.Remove(key);
                }
            }
        }
        private bool IsHidden(string key, string title)
        {
            var list = GetHiddenAbpDefaultApiList(title);
            foreach (var item in list)
            {
                if (key.Contains(item)) return true;
            }

            return false;
        }

        private List<string> GetHiddenAbpDefaultApiList(string title)
        {
            if (title == "Account Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration",
                    "/api/identity/"
                };
            }
            else if (title == "Saas Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration",
                    "/api/feature-management/features"
                };
            }
            else if (title == "Administration Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition"
                };
            }
            else if (title == "Identity Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration"
                };
            }
            else
            {
                return new List<string>() {
                    "/api/abp/api-definition"
                };
            }
        }
    }
}
=======
﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Guili.Shared.Hosting.AspNetCore
{
    public class HiddenAbpDefaultApiFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    string key = "/" + apiDescription.RelativePath;
                    var reuslt = IsHidden(key, swaggerDoc.Info.Title);
                    if (reuslt) swaggerDoc.Paths.Remove(key);
                }
            }
        }
        private bool IsHidden(string key, string title)
        {
            var list = GetHiddenAbpDefaultApiList(title);
            foreach (var item in list)
            {
                if (key.Contains(item)) return true;
            }

            return false;
        }

        private List<string> GetHiddenAbpDefaultApiList(string title)
        {
            if (title == "Account Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration",
                    "/api/identity/"
                };
            }
            else if (title == "Saas Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration",
                    "/api/feature-management/features"
                };
            }
            else if (title == "Administration Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition"
                };
            }
            else if (title == "Identity Service API")
            {
                return new List<string>() {
                    "/api/abp/api-definition",
                    "/api/abp/application-configuration"
                };
            }
            else
            {
                return new List<string>() {
                    "/api/abp/api-definition"
                };
            }
        }
    }
}
>>>>>>> git/ids4
