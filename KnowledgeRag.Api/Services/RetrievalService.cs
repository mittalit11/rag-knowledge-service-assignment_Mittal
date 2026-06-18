using KnowledgeRag.Api.Models;
using KnowledgeRag.Api.Repositories;

namespace KnowledgeRag.Api.Services;

public class RetrievalService
{
    private readonly IVectorStore _store;

    public RetrievalService(IVectorStore store)
    {
        _store = store;
    }

    private static readonly HashSet<string> StopWords =
    [
        "what","is","are","the","a","an","on","in","at","to","of",
        "does","do","did","for","with","and","or","about","tell","me"
    ];

    public Task<List<(DocumentChunk Chunk, double Score)>> RetrieveAsync(string question)
    {
        if (string.IsNullOrWhiteSpace(question))
            return Task.FromResult(new List<(DocumentChunk, double)>());

        var queryWords = question
            .ToLower()
            .Replace("?", "")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Where(w => !StopWords.Contains(w))
            .ToList();

        if (!queryWords.Any())
            return Task.FromResult(new List<(DocumentChunk, double)>());

        var results = _store.GetAll()
            .Select(chunk =>
            {
                var contentWords = chunk.Content
                    .ToLower()
                    .Split(new[] { ' ', '.', ',', '\n', '\r', ':', ';', '(', ')' },
                        StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();

                int matches = queryWords.Count(q => contentWords.Contains(q));

                double score = (double)matches / queryWords.Count;

                return (Chunk: chunk, Score: score);
            })
            .Where(x => x.Score > 0) // IMPORTANT: removes junk matches
            .OrderByDescending(x => x.Score)
            .Take(3)
            .ToList();

        return Task.FromResult(results);
    }
}