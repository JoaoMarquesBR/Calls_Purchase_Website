namespace PhoneLog
{


        public static class SwaggerAuthorizeExtensions
        {
            public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
            }
        }
    
}
