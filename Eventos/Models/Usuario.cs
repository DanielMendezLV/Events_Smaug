using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Apellido { get; set; }
        [Required]
        public String Correo { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]


        public String Nickname { get; set; }
        public int Telefono { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}