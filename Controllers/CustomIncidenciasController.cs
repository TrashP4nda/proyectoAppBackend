using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectoApi.Models; // Make sure this points to where your CustomIncidencia model is located
using proyectoApi.Data;   // Adjust this to point to where your ApplicationDbContext (or equivalent DbContext) is located
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyectoApi.Models.DTOs; // Ensure this points to where your CustomIncidenciaDTO is located

namespace proyectoApi.Controllers
{
    [ApiController]
    [Route("api/customIncidencias")] // Updated route for consistency
    public class CustomIncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomIncidenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/customIncidencias
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<customIncidencia>>> GetCustomIncidencias()
        {
            var customIncidencias = await _context.CustomIncidencias.ToListAsync();
            return Ok(customIncidencias);
        }

        // GET: api/customIncidencias/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<customIncidencia>> GetCustomIncidencia(int id)
        {
            var customIncidencia = await _context.CustomIncidencias.FindAsync(id);

            if (customIncidencia == null)
            {
                return NotFound();
            }

            return Ok(customIncidencia);
        }

        // POST: api/customIncidencias
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<customIncidencia>> PostCustomIncidencia([FromForm] CustomIncidenciaDTO customIncidenciaDTO)
        {
            var customIncidencia = new customIncidencia
            {
                // Map properties from DTO to CustomIncidencia model
                AutonomousRegion = customIncidenciaDTO.AutonomousRegion,
                CarRegistration = customIncidenciaDTO.CarRegistration,
                Cause = customIncidenciaDTO.Cause,
                CityTown = customIncidenciaDTO.CityTown,
                Direction = customIncidenciaDTO.Direction,
                endDate = customIncidenciaDTO.endDate,
                incidenceDescription = customIncidenciaDTO.incidenceDescription,
                incidenceId = customIncidenciaDTO.incidenceId,
                IncidenceLevel = customIncidenciaDTO.IncidenceLevel,
                latitude = customIncidenciaDTO.latitude,
                longitude = customIncidenciaDTO.longitude,
            };

            _context.CustomIncidencias.Add(customIncidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomIncidencia", new { id = customIncidencia.Id }, customIncidencia);
        }

        // PUT: api/customIncidencias/{incidenceId}
        [HttpPut("{incidenceId}")]
        [Authorize]
        public async Task<IActionResult> PutCustomIncidencia(string incidenceId, [FromForm] CustomIncidenciaDTO customIncidenciaDTO)
        {
            if (incidenceId != customIncidenciaDTO.incidenceId)
            {
                return BadRequest("The incidenceId does not match the incidenceId in the data.");
            }

            var customIncidencia = await _context.CustomIncidencias
                .FirstOrDefaultAsync(ci => ci.incidenceId == incidenceId);
            if (customIncidencia == null)
            {
                return NotFound();
            }

            // Map the updated properties from DTO to the entity
            customIncidencia.AutonomousRegion = customIncidenciaDTO.AutonomousRegion;
            customIncidencia.CarRegistration = customIncidenciaDTO.CarRegistration;
            customIncidencia.Cause = customIncidenciaDTO.Cause;
            customIncidencia.CityTown = customIncidenciaDTO.CityTown;
            customIncidencia.Direction = customIncidenciaDTO.Direction;
            customIncidencia.endDate = customIncidenciaDTO.endDate;
            customIncidencia.incidenceDescription = customIncidenciaDTO.incidenceDescription;
           // customIncidencia.incidenceId = customIncidenciaDTO.incidenceId; // This line might be redundant if the ID shouldn't change.
            customIncidencia.IncidenceLevel = customIncidenciaDTO.IncidenceLevel;
            customIncidencia.latitude = customIncidenciaDTO.latitude;
            customIncidencia.longitude = customIncidenciaDTO.longitude;

            _context.Entry(customIncidencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomIncidenciaExists(incidenceId))
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

        private bool CustomIncidenciaExists(string incidenceId)
        {
            return _context.CustomIncidencias.Any(e => e.incidenceId == incidenceId);
        }


        // DELETE: api/incidencias/5
        [Authorize]
        [HttpDelete("{incidenceId}")]
        public async Task<IActionResult> DeleteCustomIncidencia(string incidenceId)
        {
            // Find the incidencia entry using IncidenceID
            var incidencia = await _context.CustomIncidencias
                .FirstOrDefaultAsync(i => i.incidenceId == incidenceId);

            if (incidencia == null)
            {
                return NotFound();
            }

            _context.CustomIncidencias.Remove(incidencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomIncidenciaExists(int id)
        {
            return _context.CustomIncidencias.Any(e => e.Id == id);
        }
    }
}
