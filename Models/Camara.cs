using System.Text.Json.Serialization;

namespace proyectoApi.Models;

using System.ComponentModel.DataAnnotations;

public class Camara
{
    [Key]
    public int Id { get; set; } // Assuming you need a primary key for database

    [Required]
    [MaxLength(100)]
    public string Address { get; set; }

    [Required]
    [MaxLength(100)]
    public string CameraId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CameraName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Kilometer { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Latitude { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Longitude { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string url { get; set; }
    
    public virtual ICollection<UsuarioCamaraFavorite> FavoriteByUsuarios { get; set; }

}