using SyntriceEShop.API;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<JWTOptions>()
    .Bind(builder.Configuration.GetSection(JWTOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

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

