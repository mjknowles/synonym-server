const string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  if (builder.Environment.IsDevelopment())
  {
    options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"));
  }
  else
  {
    options.AddPolicy(name: _myAllowSpecificOrigins,
                      builder => builder.WithOrigins("https://synonym.azurewebsites.net"));
  }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");
app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
  app.UseCors();
else
  app.UseCors(_myAllowSpecificOrigins);

app.Run();
