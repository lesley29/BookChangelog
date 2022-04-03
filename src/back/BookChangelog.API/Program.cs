using BookChangelog.API.Features.Authors;
using BookChangelog.API.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateAuthorRequest.Validator>();
        fv.DisableDataAnnotationsValidation = true;
    })
    .AddJsonOptions(opts => opts.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddDbContext<BookChangelogContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Default"), npgsqlOpts => npgsqlOpts.UseNodaTime()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();