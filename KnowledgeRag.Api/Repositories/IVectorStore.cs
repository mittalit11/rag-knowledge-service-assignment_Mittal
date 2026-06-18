using KnowledgeRag.Api.Models;

namespace KnowledgeRag.Api.Repositories;

public interface IVectorStore
{
    void Add(DocumentChunk chunk);

    List<DocumentChunk> GetAll();
}