using KnowledgeRag.Api.Models;

namespace KnowledgeRag.Api.Repositories;

public class InMemoryVectorStore : IVectorStore
{
    private readonly List<DocumentChunk> _chunks = new();

    public void Add(DocumentChunk chunk)
    {
        _chunks.Add(chunk);
    }

    public List<DocumentChunk> GetAll()
    {
        return _chunks;
    }
}