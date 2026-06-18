namespace KnowledgeRag.Api.Models;

public class AskResponse
{
    public string Answer { get; set; }

    public double Confidence { get; set; }

    public List<string> Sources { get; set; }
}