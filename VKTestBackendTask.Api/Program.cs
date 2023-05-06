using Microsoft.AspNetCore.Mvc.ApiExplorer;
using VKTestBackendTask.Api;
using VKTestBackendTask.Bll;
using VKTestBackendTask.Dal;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;
    builder.Services
        .AddApi()
        .AddBll(configuration)
        .AddDal(configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    
    if (app.Environment.IsDevelopment())
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}