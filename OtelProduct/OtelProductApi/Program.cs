using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OtelProductApi.ConfigureServices;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigureServiceExtensions(builder.Configuration);

string serviceName = Assembly.GetExecutingAssembly().GetName().Name!;
string serviceVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                            ?? Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0";


builder.Logging.AddOpenTelemetry(options => options
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName, serviceVersion))
    .AddConsoleExporter()
    .AddOtlpExporter());

builder.Services.AddOpenTelemetry()
                .ConfigureResource(configure => configure
                    .AddService(serviceName, serviceVersion))
                .WithTracing(tracerBuilder => tracerBuilder
                    .AddSource("Alo")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(
                        options => {
                            options.SetDbStatementForText = true;
                            options.EnableConnectionLevelAttributes = true;
                            options.RecordException = true;
                        })
                    .AddConsoleExporter()
                    .AddOtlpExporter());

builder.Services.AddOpenTelemetry()
                .ConfigureResource(configure => configure
                    .AddService(serviceName, serviceVersion))
                .WithMetrics(metricBuilder => metricBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddOtlpExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var source = new ActivitySource("MicroserviceTraceExamp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
