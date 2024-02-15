namespace proyectoApi.Models;
using System.ComponentModel.DataAnnotations;
public class customIncidencia 
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string AutonomousRegion { get; set; }
    [Required]
    [MaxLength(100)]
    public string CarRegistration { get; set; }
    [Required]
    [MaxLength(100)]
    public string Cause { get; set; }
    [Required]
    [MaxLength(100)]
    public string CityTown { get; set; }
    [Required]
    [MaxLength(100)]
    public string Direction { get; set; }
    [Required]
    [MaxLength(100)]
    public string endDate { get; set; }
    [Required]
    [MaxLength(100)]
    public string incidenceDescription { get; set; }
    [Required]
    [MaxLength(100)]
    public string incidenceId { get; set; }
    [Required]
    [MaxLength(100)]
    public string IncidenceLevel { get; set; }
    [Required]
    [MaxLength(100)]
    public string longitude { get; set; }
    [Required]
    [MaxLength(100)]
    public string latitude { get; set; }
}