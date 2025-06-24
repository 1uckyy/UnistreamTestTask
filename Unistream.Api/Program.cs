using Microsoft.EntityFrameworkCore;
using Npgsql;
using Unistream.Api.Middleware;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Abstractions.Services;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Entities.Transaction;
using Unistream.Infrastructure;
using Unistream.Infrastructure.Repositories;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    var configuration = builder.Configuration;

    // Add services to the container.

    builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            //.Enrich.WithProcessId()
            //.Enrich.WithThreadId()
            .Enrich.WithProperty("Application", Assembly.GetExecutingAssembly().GetName().Name)
            .WriteTo.Console()
            .WriteTo.File("logs/application.log", rollingInterval: RollingInterval.Day)
    );

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(typeof(Unistream.Application.AssemblyReference).Assembly);
    });

    builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseNpgsql(new NpgsqlDataSourceBuilder(configuration.GetConnectionString(nameof(ApplicationDbContext)))
        .EnableDynamicJson()
        .Build()));

    builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
    builder.Services.AddTransient<ITransactionService, TransactionService>();
    builder.Services.AddTransient<IBalanceRepository, BalanceRepository>();
    builder.Services.AddTransient<IBalanceService, BalanceService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            Log.Information("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while migrating the database.");
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}