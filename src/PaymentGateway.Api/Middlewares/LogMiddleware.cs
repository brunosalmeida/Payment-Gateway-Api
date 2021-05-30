using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Infrastructure.Dispatch;
using PaymentGateway.Infrastructure.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PaymentGateway.Api.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LogOptions options;
        public LogMiddleware(RequestDelegate next, LogOptions options)
        {
            _next = next;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context, DispatchService dispatchService)
        {
            LogEntry log = new LogEntry();
            log.ProjectKey = this.options.ProjectKey;
            log.Date = DateTime.Now;

            log.Tags = new Dictionary<string, object>()
            {
                { "Path", context.Request.Path },
                { "Scheme", context.Request.Scheme },
                { "Host", context.Request.Host.ToString() },
                { "RemoteIpAddress", context.Connection.RemoteIpAddress.ToString() },
                { "QueryString", context.Request.QueryString.Value },
                { "RequestLength",  context.Request.ContentLength ?? 0},
            };
            

            if (context.Request.Query != null)
            {
                foreach (var query in context.Request.Query)
                {
                    AddOrUpdateTag(log, $"_query._{query.Key}", query.Value);
                }
            }

            context.Items.Add("logentry", log);

            try
            {
                await _next(context);
                log.LogLevel = LogLevel.Debug;
            }
            catch (Exception ex)
            {
                log.LogLevel = LogLevel.Error;
                log.Tags.Add("Exception", ex.ToString());
                throw;
            }
            finally
            {
                context.Items.Remove("logentry");
                this.PostExecutionLog(context, log);
                dispatchService.Dispatch(log, this.options.QueueToSend);
            }
        }

        private void AddOrUpdateTag(LogEntry log, string key, object value)
        {
            if (key != null && key.Contains("__") == false)
            {
                if (log.Tags.ContainsKey(key))
                    log.Tags[key] = value.ToString();
                else
                    log.Tags.Add(key, value.ToString());
            }
        }

        private void PostExecutionLog(HttpContext context, LogEntry log)
        {
            if (context.Items != null && context.Items.Any())
            {
                foreach (var item in context.Items)
                {
                    if (item.Key is string)
                        AddOrUpdateTag(log, (string)item.Key, item.Value);
                }
            }
            AddOrUpdateTag(log, "ResponseLength", context.Response?.ContentLength ?? 0);
        }
    }
}