using Serilog;
using Serilog.Events;
using System.Reflection;
using WarehouseMovementService.APIs;
using WarehouseMovementService.Application;
using WarehouseMovementService.Infrastructure;
using WarehouseMovementService.Startup;

Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var applicationLayerAssembly = Assembly.Load("WarehouseMovementService.Application");
    builder.UseWebApplication(applicationLayerAssembly);

    builder.Services
                .AddApplication()
                .AddInfrastructure(connectionString: builder.Configuration.GetConnectionString("DefaultConnectionString"),
                                    consumerAssemblies: applicationLayerAssembly);

    var app = builder.Build();

    await app.Services.EnsureDBCreated();

    app.UseWebApplication();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapAPI();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
