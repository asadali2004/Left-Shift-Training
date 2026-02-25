namespace SimpleMVC.Middleware
{
    public class ValidateNameLength
    {
        private readonly RequestDelegate _next;
        public ValidateNameLength(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Only validate POST requests to Employee/Create
            if (context.Request.Method == "POST" && 
                context.Request.Path.StartsWithSegments("/Employee/Create"))
            {
                context.Request.EnableBuffering();
                if (context.Request.HasFormContentType)
                {
                    var form = await context.Request.ReadFormAsync();
                    var name = form["Name"].ToString();

                    // Validate name length (should be > 2)
                    if (!string.IsNullOrWhiteSpace(name) && name.Length <= 2)
                    {
                        // Redirect back with error message
                        context.Response.Redirect($"/Employee/Create?error=Name must be greater than 2 characters. You entered: '{name}'");
                        return;
                    }

                    // Reset the request body position
                    context.Request.Body.Position = 0;
                }
            }

            await _next(context);
        }
    }
}
