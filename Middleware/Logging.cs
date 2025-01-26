using System.Diagnostics;

namespace StudentManagementSystem.Middleware
{
    public class Logging
    {
        private readonly RequestDelegate _request;
        public Logging(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Debug.WriteLine($"Incoming Requests are {context.Request.Method} {context.Request.Path}");

            var stopwatch = new Stopwatch();
            await _request(context);
            stopwatch.Stop();

            //Debug.WriteLine($"{context.User.Identity.Name}");
            Debug.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
            Debug.WriteLine($"Request Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
