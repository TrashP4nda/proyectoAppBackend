﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoApi.Models
{
    public class UsuarioCamaraFavorite
    {
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Key, Column(Order = 1)]
        public int CamaraId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Camara Camara { get; set; }
    }
}