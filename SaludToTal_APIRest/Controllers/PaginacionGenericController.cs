using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.Models;
using SaludToTal_APIRest.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity> : ControllerBase where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericController(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }


    // GET: api/Entidad?page=1&pageSize=10
    [HttpGet("paginados")]
    public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
    {
        int totalItems = await _dbSet.CountAsync();
        var items = await _dbSet
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new PaginatedResponse<TEntity>
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            Data = items
        };

        return Ok(response);
    }


    // GET: api/Entidad/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    // POST: api/Entidad
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, entity);
    }

    // PUT: api/Entidad/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Puedes crear una función para obtener el ID si querés más control:
    private int GetEntityId(TEntity entity)
    {
        var prop = typeof(TEntity).GetProperty("Id");
        return (int)(prop?.GetValue(entity) ?? 0);
    }
}

