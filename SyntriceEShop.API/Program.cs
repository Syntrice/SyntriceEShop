// See https://aka.ms/new-console-template for more information

using SyntriceEShop.API;

var builder = WebApplication.CreateBuilder(args);

builder.SetupDatabase();

var app = builder.Build();

await app.RunAsync();

