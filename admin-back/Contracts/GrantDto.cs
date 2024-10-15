namespace admin_back.Contracts;

public record GrantDto(int? Id, string? Title, string? SourceUrl, object? FilterValues);