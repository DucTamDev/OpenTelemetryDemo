using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OtelIdentityApi.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigureServiceExtensions(builder.Configuration);

builder.Logging.AddOpenTelemetry(options => options
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: "Otel-Indentity", serviceVersion: "1.0.0.0"))
    .AddConsoleExporter()
    .AddOtlpExporter());

builder.Services.AddOpenTelemetry()
                .ConfigureResource(configure => configure
                    .AddService(serviceName: "Otel-Indentity"))
                .WithTracing(tracerBuilder => tracerBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter());

builder.Services.AddOpenTelemetry()
                .ConfigureResource(configure => configure
                    .AddService(serviceName: "Otel-Indentity"))
                .WithMetrics(metricBuilder => metricBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
