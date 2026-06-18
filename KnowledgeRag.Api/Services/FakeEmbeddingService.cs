namespace KnowledgeRag.Api.Services;

public class FakeEmbeddingService : IEmbeddingService
{
    public Task<float[]> GenerateAsync(string text)
    {
        return Task.FromResult(new float[10]);
    }
}