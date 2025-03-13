// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Authentication.JwtBearer;
using SyntriceEShop.API;

var builder = WebApplication.CreateBuilder(args);

builder.SetupDatabase();
builder.SetupRepositories();
builder.SetupServices();
builder.SetupAuthentication();

builder.Services.AddControllers();

builder.SetupSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();

