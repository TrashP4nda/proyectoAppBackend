using Microsoft.AspNetCore.Authorization;

namespace proyectoApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using proyectoApi.Models; // Import your Incidencia model namespace
using proyectoApi.Data;   // Import your ApplicationDbContext namespace
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/incidencias")] // Updated route to be more RESTful
public class IncidenciasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IncidenciasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/incidencias
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Incidencia>>> GetIncidencias()
    {
        var incidencias = await _context.Incidencias.ToListAsync();
        return Ok(incidencias);
    }

    // GET: api/incidencias/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Incidencia>> GetIncidencia(int id)
    {
        var incidencia = await _context.Incidencias.FindAsync(id);

        if (incidencia == null)
        {
            return NotFound();
        }

        return Ok(incidencia);
    }

    // POST: api/incidencias
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Incidencia>> PostIncidencia([FromForm] Incidencia incidencia)
    {
        _context.Incidencias.Add(incidencia);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetIncidencia", new { id = incidencia.Id }, incidencia);
    }

    // PUT: api/incidencias/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncidencia(int id, Incidencia incidencia)
    {
        if (id != incidencia.Id)
        {
            return BadRequest();
        }

        _context.Entry(incidencia).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IncidenciaExists(id))
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

    // DELETE: api/incidencias/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncidencia(int id)
    {
        var incidencia = await _context.Incidencias.FindAsync(id);
        if (incidencia == null)
        {
            return NotFound();
        }

        _context.Incidencias.Remove(incidencia);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IncidenciaExists(int id)
    {
        return _context.Incidencias.Any(e => e.Id == id);
    }
}
