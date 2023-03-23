using synonym_server;

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

/*
Can you give me 1000 words whose synonyms would be fun to guess as a game? I only need the words and their parts of speech. no need to list the synonyms. can you output it as a JSON array? The frequency of the first letter of each word should match the frequency of the first letter of all words in the general English language.
*/
Random random = new Random();
app.MapGet("/stem", () =>
{
  var word = Stems.Words[random.Next(0, Stems.Words.Count)];
  return new StemResponse(word.word.ToLower(), word.partOfSpeech);
}
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
  app.UseCors();
else
  app.UseCors(_myAllowSpecificOrigins);

app.Run();
