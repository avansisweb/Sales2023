using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Display(Name = "Nombre Empresa", ShortName = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "Longitud máxima del Nombre debe ser de 100 caracteres", MinimumLength = 2)]
        public string NombreEmpresa { get; set; } = null!;

        public int TipoDocumentoId { get; set; } = 0;

        [Display(Name = "Nit Empresa", ShortName = "Nit")]
        public string ?Nit { get; set; }

        public int TipoResponsabilidadTributariaId { get; set; } = 0;

        [Display(Name = "Dirección Empresa", ShortName = "Dirección")]
        public string ?DireccionEmpresa { get; set; }

        [Display(Name = "Teléfono Empresa", ShortName = "Teléfono")]
        public string ?TelefonoEmpresa { get; set; }

        [Display(Name = "Email", ShortName = "Email")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Estado", ShortName = "Estado")]
        public bool Estado { get; set; }
    }
}
