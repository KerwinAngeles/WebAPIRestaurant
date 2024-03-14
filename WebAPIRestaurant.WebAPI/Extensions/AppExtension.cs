namespace WebAPIRestaurant.WebAPI.Extensions
{
    public static class AppExtension
    {
        public static void AddSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApp API");
            });
        }
    }
}
