using ErrorHandling.Errors;
using ErrorHandling.Filters;
using ErrorHandling.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Filters Approach used to add CustomErrorFilterAttribute for each controller :
//builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());

//Injecting our custom factory to ovverride ProblemDetails Class :
builder.Services.AddSingleton<ProblemDetailsFactory, ErrorsProblemDetailsFactory>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Uncomment this line to use Errors Handling Middleware :
//app.UseMiddleware<ErrorHandlingMiddleware>();

//To Use ErrorsController uncomment this line :
//app.UseExceptionHandler("/error");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
