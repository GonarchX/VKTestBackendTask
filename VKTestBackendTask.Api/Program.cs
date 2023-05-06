using VKTestBackendTask.Api;
using VKTestBackendTask.Bll;
using VKTestBackendTask.Dal;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;
    builder.Services
        .AddApi()
        .AddBll()
        .AddDal(configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}