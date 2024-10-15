namespace admin_back.Models;

public class GrantFilters
{
    public List<int> ProjectDirection { get; set; }
    public int Amount { get; set; }
    public List<int> LegalForm { get; set; }
    public int Age { get; set; }
    public List<int> CuttingOffCriteria { get; set; }
}