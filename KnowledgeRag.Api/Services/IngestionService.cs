using KnowledgeRag.Api.Models;
using KnowledgeRag.Api.Repositories;

namespace KnowledgeRag.Api.Services;

public class IngestionService
{
    private readonly ChunkingService _chunking;
    private readonly IEmbeddingService _embedding;
    private readonly IVectorStore _store;

    public IngestionService(
        ChunkingService chunking,
        IEmbeddingService embedding,
        IVectorStore store)
    {
        _chunking = chunking;
        _embedding = embedding;
        _store = store;
    }

    public async Task IngestAsync(string folder)
    {
        var files = Directory.GetFiles(folder);

        foreach (var file in files)
        {
            var text =
                await File.ReadAllTextAsync(file);

            var chunks =
                _chunking.Chunk(text);

            foreach (var chunk in chunks)
            {
                var vector =
                    await _embedding.GenerateAsync(chunk);

                _store.Add(new DocumentChunk
                {
                    Id = Guid.NewGuid().ToString(),
                    DocumentId =
                        Path.GetFileName(file),
                    Content = chunk,
                    Embedding = vector
                });
            }
        }
    }
}