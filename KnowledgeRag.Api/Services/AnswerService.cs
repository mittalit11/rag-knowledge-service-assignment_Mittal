using KnowledgeRag.Api.Models;

namespace KnowledgeRag.Api.Services;

public class AnswerService
{
    private readonly RetrievalService _retrieval;

    public AnswerService(RetrievalService retrieval)
    {
        _retrieval = retrieval;
    }

    public async Task<AskResponse> AskAsync(string question)
    {
        var results = await _retrieval.RetrieveAsync(question);

        // ❗ IMPORTANT: no results case
        if (results == null || results.Count == 0)
        {
            return new AskResponse
            {
                Answer = "No confident answer found.",
                Confidence = 0,
                Sources = []
            };
        }

        var best = results.First();

        // ❗ threshold for "no answer"
        if (best.Score < 0.30)
        {
            return new AskResponse
            {
                Answer = "No confident answer found.",
                Confidence = best.Score,
                Sources = []
            };
        }

        return new AskResponse
        {
            Answer = string.Join("\n\n",
                results
                    .Where(r => r.Score > 0)
                    .Select(r => r.Chunk.Content)),

            Confidence = Math.Round(best.Score, 2),

            Sources = results
                .Where(r => r.Score > 0)
                .Select(r => r.Chunk.DocumentId)
                .Distinct()
                .ToList()
        };
    }
}