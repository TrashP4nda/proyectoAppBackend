
namespace proyectoApi.Models;
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }
    
    [Required]
    [MaxLength(255)] 
    [EmailAddress]   
    public string Email { get; set; }
    
    public virtual ICollection<UsuarioCamaraFavorite> FavoriteCameras { get; set; }
    public virtual ICollection<UsuarioIncidenciaFavorite> FavoriteIncidencias { get; set; }
}
