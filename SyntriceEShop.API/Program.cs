// See https://aka.ms/new-console-template for more information

using SyntriceEShop.API;
using SyntriceEShop.API.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.SetupDatabase();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();

