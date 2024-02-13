using Microsoft.AspNetCore.Authorization;
using proyectoApi.Services;

namespace proyectoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using proyectoApi.Models; // Import your Incidencia model namespace
using proyectoApi.Data;   // Import your ApplicationDbContext namespace
using proyectoApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;



    [ApiController]
    [Route("api/usuarios")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        public UserController(ApplicationDbContext context , IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Create a new user
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromForm] UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Username = usuarioDTO.Username,
                PasswordHash = usuarioDTO.PasswordHash, // Note: Password should be hashed
                Email = usuarioDTO.Email
            };
            
            
            await _emailService.SendEmailAsync( usuario.Email,"Welcome to Our Service", "<h1>Thank you for registering!</h1>");
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Return the newly created user
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }


        // Get a user by ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        // Get all users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id) 
                return BadRequest("ID mismatch");

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) 
            {
                return NotFound();
            }

            // Map the DTO to the entity
            usuario.Username = usuarioDTO.Username;
            // Assuming you're hashing the password somewhere else before setting it
            usuario.PasswordHash = usuarioDTO.PasswordHash;
            usuario.Email = usuarioDTO.Email;
            // Map other properties as needed

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id == id))
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


        // Delete a user
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginModel login)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Username == login.Username && u.PasswordHash == login.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            // Create an object that includes both the token and user information
            var response = new
            {
                token,
                user = new
                {
                    Id = user.Id,
                    Username = user.Username,
                    // Add other user information as needed
                }
            };

            return Ok(response);
        }
        
        [Authorize]
        [HttpPost("addCameraFavorite/{userId}")]
        public async Task<IActionResult> AddCameraAndFavorite(int userId, [FromForm] CamaraDTO cameraData)
        {
            // First, check if the user exists
            var user = await _context.Usuarios.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the camera already exists
            var camera = await _context.Camaras.FirstOrDefaultAsync(c => c.CameraId == cameraData.CameraId);

            // If the camera doesn't exist, add it
            if (camera == null)
            {
                camera = new Camara
                {
                    CameraId = cameraData.CameraId,
                    Address = cameraData.Address,
                    CameraName = cameraData.CameraName,
                    Kilometer = cameraData.Kilometer,
                    Latitude = cameraData.Latitude,
                    Longitude = cameraData.Longitude,
                    urlImage = cameraData.urlImage
                };
                _context.Camaras.Add(camera);
                // Do not await SaveChangesAsync() here; we'll save later after adding favorites
            }

            // Check if the camera is already a favorite
            bool isAlreadyFavorite = await _context.UsuarioCamaraFavorites
                .AnyAsync(f => f.UsuarioId == userId && f.CamaraId == camera.Id);
    
            if (!isAlreadyFavorite)
            {
                // Link the camera to the user as a favorite
                var favorite = new UsuarioCamaraFavorite
                {
                    UsuarioId = userId,
                    Camara = camera // Directly assign the camera entity
                };
                _context.UsuarioCamaraFavorites.Add(favorite);
            }

            // Now save changes for both the new camera and the new favorite
            await _context.SaveChangesAsync();

            return Ok();
        }

        
        [Authorize]
        [HttpPost("addIncidenciaFavorite/{userId}")]
        public async Task<IActionResult> AddIncidenciaAndFavorite(int userId, [FromForm] IncidenciaDTO incidenciaData)
        {
         
            var user = await _context.Usuarios.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

          
            var incidencia = await _context.Incidencias
                .FirstOrDefaultAsync(i => i.incidenceId == incidenciaData.incidenceId);

          
            if (incidencia == null)
            {
                incidencia = new Incidencia
                {
                    AutonomousRegion = incidenciaData.AutonomousRegion,
                    CarRegistration = incidenciaData.CarRegistration,
                    Cause = incidenciaData.Cause,
                    CityTown = incidenciaData.CityTown,
                    Direction = incidenciaData.Direction,
                    endDate = incidenciaData.endDate,
                    incidenceDescription = incidenciaData.incidenceDescription,
                    incidenceId = incidenciaData.incidenceId,
                    IncidenceLevel = incidenciaData.IncidenceLevel
                };
                _context.Incidencias.Add(incidencia);
              
            }

        
            bool isAlreadyFavorite = await _context.UsuarioIncidenciaFavorites
                .AnyAsync(f => f.UsuarioId == userId && f.IncidenciaId == incidencia.Id);

            if (!isAlreadyFavorite)
            {
              
                var favorite = new UsuarioIncidenciaFavorite
                {
                    UsuarioId = userId,
                    Incidencia = incidencia 
                };
                _context.UsuarioIncidenciaFavorites.Add(favorite);
            }

         
            await _context.SaveChangesAsync();

            return Ok();
        }

        
        [Authorize]
        [HttpGet("{userId}/favorites")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            // Retrieving favorite cameras
            var favoriteCameras = _context.UsuarioCamaraFavorites
                .Where(ucf => ucf.UsuarioId == userId)
                .Select(ucf => ucf.Camara)
                .ToList();

            // Retrieving favorite incidencias
            var favoriteIncidencias = _context.UsuarioIncidenciaFavorites
                .Where(uif => uif.UsuarioId == userId)
                .Select(uif => uif.Incidencia)
                .ToList();

            var result = new 
            {
                FavoriteCameras = favoriteCameras,
                FavoriteIncidencias = favoriteIncidencias
            };

            return Ok(result);
        }

        
        [Authorize]
        [HttpDelete("removeFavoriteCamera/{userId}/{cameraId}")]
        public async Task<IActionResult> RemoveFavoriteCamera(int userId, String cameraId)
        {
            
            var favoriteCamera = await _context.UsuarioCamaraFavorites
                .Where(f => f.UsuarioId == userId)
                .Include(f => f.Camara) 
                .FirstOrDefaultAsync(f => f.Camara.CameraId == cameraId);

            if (favoriteCamera == null)
            {
                return NotFound("Favorite camera not found.");
            }

            _context.UsuarioCamaraFavorites.Remove(favoriteCamera);
            await _context.SaveChangesAsync();

            return Ok();
        }

        
        [Authorize]
        [HttpDelete("removeFavoriteIncidencia/{userId}/{incidenciaId}")]
        public async Task<IActionResult> RemoveFavoriteIncidencia(int userId, String incidenciaId)
        {
         
            var favoriteIncidencia = await _context.UsuarioIncidenciaFavorites
                .Where(f => f.UsuarioId == userId)
                .Include(f => f.Incidencia)
                .FirstOrDefaultAsync(f => f.Incidencia.incidenceId == incidenciaId);

            if (favoriteIncidencia == null)
            {
                return NotFound("Favorite incidencia not found.");
            }

            _context.UsuarioIncidenciaFavorites.Remove(favoriteIncidencia);
            await _context.SaveChangesAsync();

            return Ok();
        }

        
        

        private string GenerateJwtToken(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_here")); // Replace 'YourSecretKey' with your actual secret key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
           
            };

            var token = new JwtSecurityToken(
                issuer: "prueba.com",    
                audience: "best-api",  
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }

