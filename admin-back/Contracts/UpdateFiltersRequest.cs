using System.Text.Json;

namespace admin_back.Contracts;

public class UpdateFiltersRequest
{
    public FiltersData Data { get; set; }
}

public class FiltersData
{
    public JsonElement Project_direction { get; set; }
    public int Amount { get; set; }
    public JsonElement Legal_form { get; set; }
    public int Age { get; set; }
    public JsonElement Cutting_off_criteria { get; set; }
}