namespace admin_back.Contracts;

public class GetGrantsRequest
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

// (string? Search, int? Page, int? PageSize);}