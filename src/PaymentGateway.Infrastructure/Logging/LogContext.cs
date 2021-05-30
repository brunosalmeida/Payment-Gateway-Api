// using Microsoft.AspNetCore.Mvc;
//
// namespace PaymentGateway.Infrastructure.Logging
// {
//     public static class LogContext
//     {
//         public static void AddToLogContext(this ControllerBase controllerBase, string key, object data) =>
//             ((LogEntry) controllerBase.HttpContext.Items["logentry"]).Tags.Add(key, data);
//     }
// }