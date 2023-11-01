using MagicVilla;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Logger(lc => lc
		.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Error)
		.WriteTo.File("logs/error/log.txt", rollingInterval: RollingInterval.Day))
	.WriteTo.File("logs/app/log.txt", rollingInterval: RollingInterval.Day)
	.WriteTo.Console()
	.CreateLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddTransient<IVillaService, VillaService>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();