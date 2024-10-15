using admin_back.Controllers;

namespace admin_back.Contracts;

public class GetGrantsResponse(List<GrantDto> grants, int totalCount)
{
    public List<GrantDto> Grants { get; set; } = grants;
    public int TotalCount { get; set; } = totalCount;
}

// (List<GrantDto> grants);}