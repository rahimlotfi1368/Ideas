namespace API.Middlewares
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public OptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }

        private Task BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.StatusCode = 200;
                return context.Response.WriteAsync("OK");
            }

            return _next.Invoke(context);
        }
    }
}

