<<<<<<< HEAD
﻿using Guili.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Guili.SaasService
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            var assemblyName = typeof(Program).Assembly.GetName().Name;

            SerilogConfigurationHelper.Configure(assemblyName);

            try
            {
                Log.Information($"Starting {assemblyName}.");
                var app = await ApplicationBuilderHelper.BuildApplicationAsync<SaasServiceHttpApiHostModule>(args);
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
=======
﻿using Guili.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Guili.SaasService
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            var assemblyName = typeof(Program).Assembly.GetName().Name;

            SerilogConfigurationHelper.Configure(assemblyName);

            try
            {
                Log.Information($"Starting {assemblyName}.");
                var app = await ApplicationBuilderHelper.BuildApplicationAsync<SaasServiceHttpApiHostModule>(args);
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
>>>>>>> git/ids4
