using System.Text.Json;

namespace admin_back.Models;

public class Grant
{
    public Grant(string title, string sourceUrl)
    {
        Title = title;
        SourceUrl = sourceUrl;
    }
    
    public int Id { get; init; }
    public string Title { get; init; } 
    public string SourceUrl { get; init; } 
    public JsonElement ProjectDirections { get; set; }
    public int Amount { get; set; }
    public JsonElement LegalForms { get; set; }
    public int Age { get; set; }
    public JsonElement CuttingOffCriteria { get; set; }
}