using Microsoft.AspNetCore.Authorization;
using proyectoApi.Models.DTOs;

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
    public async Task<ActionResult<Incidencia>> PostIncidencia([FromForm] IncidenciaDTO incidenciaDTO)
    {

        var incidencia = new Incidencia
        {
         AutonomousRegion   = incidenciaDTO.AutonomousRegion,
         CarRegistration = incidenciaDTO.CarRegistration,
         Cause = incidenciaDTO.Cause,
         CityTown = incidenciaDTO.CityTown,
         Direction = incidenciaDTO.Direction,
         endDate = incidenciaDTO.Direction,
         incidenceDescription = incidenciaDTO.incidenceDescription,
         incidenceID = incidenciaDTO.incidenceID,
         IncidenceLevel = incidenciaDTO.IncidenceLevel
        };
        
        _context.Incidencias.Add(incidencia);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetIncidencia", new { id = incidencia.Id }, incidencia);
    }

    // PUT: api/incidencias/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncidencia(int id, [FromForm] IncidenciaDTO incidencia)
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
    [HttpDelete("{incidenceId}")]
    public async Task<IActionResult> DeleteIncidencia(string incidenceId)
    {
        // Find the incidencia entry using IncidenceID
        var incidencia = await _context.Incidencias
            .FirstOrDefaultAsync(i => i.incidenceID == incidenceId);

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
