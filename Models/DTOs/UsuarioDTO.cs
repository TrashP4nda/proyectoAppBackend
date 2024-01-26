namespace proyectoApi.Models.DTOs;

public class UsuarioDTO
{
    
    public int Id { get; set; }

  
    public string Username { get; set; }

   
    public string PasswordHash { get; set; }
    
 
    public string Email { get; set; }
}