using MongoDB.Driver;
using MoodifyAPI.Services.Playlist;
using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddSingleton(new ChatClient(model: "gpt-4o-mini", builder.Configuration.GetValue<string>("OpenAIKey")));
builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetValue<string>("MongoDb")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
