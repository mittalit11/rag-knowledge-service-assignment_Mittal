using KnowledgeRag.Api.Repositories;
using KnowledgeRag.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IVectorStore,
    InMemoryVectorStore>();

builder.Services.AddSingleton<IEmbeddingService,
    FakeEmbeddingService>();

builder.Services.AddSingleton<ChunkingService>();
builder.Services.AddSingleton<IngestionService>();
builder.Services.AddSingleton<RetrievalService>();
builder.Services.AddSingleton<AnswerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowAll");
app.MapControllers();

using var scope =
    app.Services.CreateScope();

var ingestion =
    scope.ServiceProvider
        .GetRequiredService<IngestionService>();

await ingestion.IngestAsync(
    Path.Combine(
        app.Environment.ContentRootPath,
        "Data",
        "docs"));

app.Run();