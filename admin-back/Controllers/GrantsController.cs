
using System.Text.Json;
using admin_back.Contracts;
using admin_back.DataAccess;
using admin_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace admin_back.Controllers;

[ApiController]
[Route("admin/api/v1/grants")]
public class GrantsController(GrantsDbContext dbContext) : ControllerBase
{
    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CreateGrantRequest request, CancellationToken ct)
    // {
    //     var grant = new Grant(request.Title, request.SourceUrl);
    //
    //     await _dbContext.Grants.AddAsync(grant, ct);
    //     await _dbContext.SaveChangesAsync(ct);
    //     
    //     return Ok();
    // }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetGrants([FromQuery] GetGrantsRequest request, CancellationToken ct)
    {
        // var grantsQuery = dbContext.Grants
        //     .Where(g => string.IsNullOrWhiteSpace(request.Search) ||
        //                 g.Title.ToLower().Contains(request.Search.ToLower()));
        //
        // var grantDtos = await grantsQuery
        //     .Select(g => new GrantDto(g.Id, g.Title, g.SourceUrl))
        //     .ToListAsync(cancellationToken: ct);
        //
        // return Ok(new GetGrantsResponse(grantDtos));
        
        var grantsQuery = dbContext.Grants.AsQueryable();

    // Поиск по заголовку, если указано
    if (!string.IsNullOrWhiteSpace(request.Search))
    {
        grantsQuery = grantsQuery.Where(g => g.Title.ToLower().Contains(request.Search.ToLower()));
    }

    var totalCount = await grantsQuery.CountAsync(ct);

    var grantDtos = await grantsQuery
        .Skip((request.Page - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(g => new
        {
            id = g.Id,
            title = g.Title,
            source_url = g.SourceUrl,
            filter_values = new
            {
                project_direction = g.ProjectDirections,  // jsonb массив
                amount = g.Amount,
                legal_form = g.LegalForms,               // jsonb массив
                age = g.Age,
                cutting_off_criteria = g.CuttingOffCriteria // jsonb массив
            }
        })
        .ToListAsync(ct);

    var response = new
    {
        grants = grantDtos,
        filters_mapping = new
        {
            age = new
            {
                title = "Возраст участников",
                mapping = new { }
            },
            project_direction = new
            {
                title = "Направление проекта",
                mapping = new Dictionary<string, object>
                {
                    { "0", new { title = "Не указано" } },
                    { "1", new { title = "Выявление и поддержка молодых талантов" } },
                    { "2", new { title = "Защита прав и свобод" } },
                    { "3", new { title = "Охрана здоровья" } }
                }
            },
            legal_form = new
            {
                title = "Форма юридического лица",
                mapping = new Dictionary<string, object>
                {
                    { "0", new { title = "Не указано" } },
                    { "1", new { title = "Юр. лицо" } },
                    { "2", new { title = "Физ. лицо" } }
                }
            },
            cutting_off_criteria = new
            {
                title = "Отсекающие критерии",
                mapping = new Dictionary<string, object>
                {
                    { "0", new { title = "Не указано" } },
                    { "1", new { title = "Для школьников" } },
                    { "2", new { title = "Для студентов" } },
                    { "3", new { title = "Для аспирантов" } }
                }
            },
            amount = new
            {
                title = "Сумма",
                mapping = new { }
            }
        },
        filters_order = new[]
        {
            "project_direction",
            "amount",
            "legal_form",
            "age",
            "cutting_off_criteria"
        },
        meta = new
        {
            current_page = request.Page,
            total_pages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        }
    };

    return Ok(response);
    }

    [Authorize]
    [HttpGet("{grant_id}")]
    public async Task<IActionResult> GetGrantById(int grant_id, CancellationToken ct)
    {
        // Получаем грант по его ID
        var grant = await dbContext.Grants
            .Where(g => g.Id == grant_id)
            .Select(g => new
            {
                id = g.Id,
                title = g.Title,
                source_url = g.SourceUrl,
                filter_values = new
                {
                    project_direction = g.ProjectDirections,  // jsonb массив
                    amount = g.Amount,
                    legal_form = g.LegalForms,               // jsonb массив
                    age = g.Age,
                    cutting_off_criteria = g.CuttingOffCriteria // jsonb массив
                }
            })
            .FirstOrDefaultAsync(ct);

        // Если грант не найден, возвращаем 404
        if (grant == null)
        {
            return NotFound();
        }

        var response = new
        {
            grant = grant,
            filters_mapping = new
            {
                amount = new
                {
                    title = "Сумма",
                    mapping = new { }
                },
                age = new
                {
                    title = "Возраст участников",
                    mapping = new { }
                },
                project_direction = new
                {
                    title = "Направление проекта",
                    mapping = new Dictionary<string, object>
                    {
                        { "0", new { title = "Не указано" } },
                        { "1", new { title = "Выявление и поддержка молодых талантов" } },
                        { "2", new { title = "Защита прав и свобод" } },
                        { "3", new { title = "Охрана здоровья" } }
                    }
                },
                legal_form = new
                {
                    title = "Отсекающие критерии",
                    mapping = new Dictionary<string, object>
                    {
                        { "0", new { title = "Не указано" } },
                        { "1", new { title = "Юр. лицо" } },
                        { "2", new { title = "Физ. лицо" } }
                    }
                },
                cutting_off_criteria = new
                {
                    title = "Отсекающие критерии",
                    mapping = new Dictionary<string, object>
                    {
                        { "0", new { title = "Не указано" } },
                        { "1", new { title = "Для школьников" } },
                        { "2", new { title = "Для студентов" } },
                        { "3", new { title = "Для аспирантов" } }
                    }
                }
            },
            filters_order = new[]
            {
                "project_direction",
                "amount",
                "legal_form",
                "age",
                "cutting_off_criteria"
            }
        };

        return Ok(response);
    }

    [Authorize]
    [HttpPut("{grant_id}/filters")]
    public async Task<IActionResult> UpdateGrantFilters(int grant_id, [FromBody] UpdateFiltersRequest request,
        CancellationToken ct)
    {
        // Получаем грант по его ID
        var grant = await dbContext.Grants.FindAsync(grant_id);

        // Если грант не найден, возвращаем 404
        if (grant == null)
        {
            return NotFound();
        }

        // Обновляем поля гранта
        grant.ProjectDirections = request.Data.Project_direction; // предполагаем, что это массив
        grant.Amount = request.Data.Amount;
        grant.LegalForms = request.Data.Legal_form; // предполагаем, что это массив
        grant.Age = request.Data.Age;
        grant.CuttingOffCriteria = request.Data.Cutting_off_criteria; // предполагаем, что это массив

        // Сохраняем изменения в базе данных
        await dbContext.SaveChangesAsync(ct);

        // Возвращаем статус 204 No Content
        return NoContent();
    }
}