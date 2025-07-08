using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.Models;
using SaludToTal_APIRest.Models.DTOs;

namespace SaludToTal_APIRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProfesionalController(ApplicationDbContext context) 
        { 
            _context = context;
        }


        [HttpGet("paginados")]
        public async Task<IActionResult> GetPaginados(int page = 1, int pageSize = 10)
        {
            var totalItems = await _context.Profesionales.CountAsync();
            var items = await _context.Profesionales
                .Include(p => p.usuario)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var resultado = new PaginatedResponse<Profesional>
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Data = items
            };

            return Ok(resultado);
        }

        // GET: api/Profesional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesionales()
        {
            return await _context.Profesionales
                .Include(p => p.usuario)
                .ToListAsync();
        }

        // GET: api/Profesional/paginado?page=1&pageSize=10
        [HttpGet("paginado")]
        public async Task<ActionResult<PaginatedResponse<Profesional>>> GetProfesionalesPaginados(
         [FromQuery] int page = 1,
         [FromQuery] int pageSize = 10)
{
            var totalItems = await _context.Profesionales.CountAsync();

         var profesionales = await _context.Profesionales
        .Include(p => p.usuario)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

         var response = new PaginatedResponse<Profesional>
         {
        Data = profesionales,
        TotalItems = totalItems,
        Page = page,
        PageSize = pageSize
         };

            return Ok(response);
        }


        // GET: api/Profesional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _context.Profesionales
                .Include(p => p.usuario)
                .FirstOrDefaultAsync(p => p.IdProfesional == id);

            if (profesional == null)
            {
                return NotFound();
            }

            return profesional;
        }

        // POST: api/Profesional
        [HttpPost]
        public async Task<ActionResult<Profesional>> PostProfesional(Profesional profesional)
        {
            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesional), new { id = profesional.IdProfesional }, profesional);
        }

        // PUT: api/Profesional/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesional(int id, Profesional profesional)
        {
            if (id != profesional.IdProfesional)
            {
                return BadRequest();
            }

            _context.Entry(profesional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesionalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Profesional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }

            _context.Profesionales.Remove(profesional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesionalExists(int id)
        {
            return _context.Profesionales.Any(p => p.IdProfesional == id);
        }
    }
}
