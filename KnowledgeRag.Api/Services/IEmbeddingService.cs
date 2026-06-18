namespace KnowledgeRag.Api.Services;

public interface IEmbeddingService
{
    Task<float[]> GenerateAsync(string text);
}