using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Logger(lc => lc
		.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Warning)
		.WriteTo.File("logs/warn/log.txt", rollingInterval: RollingInterval.Day))
	.WriteTo.File("logs/app/log.txt", rollingInterval: RollingInterval.Day)
	.WriteTo.Console()
	.CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(option =>
{
	// option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();