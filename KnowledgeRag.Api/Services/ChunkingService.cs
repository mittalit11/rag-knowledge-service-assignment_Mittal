namespace KnowledgeRag.Api.Services;

public class ChunkingService
{
    public List<string> Chunk(string content)
    {
        return content
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }
}