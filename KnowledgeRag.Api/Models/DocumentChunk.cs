namespace KnowledgeRag.Api.Models;

public class DocumentChunk
{
    public string Id { get; set; }

    public string DocumentId { get; set; }

    public string Content { get; set; }

    public float[] Embedding { get; set; }
}