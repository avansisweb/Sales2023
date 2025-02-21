using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Shared.Entities
{
    public class Cuenta
    {
        [Key]
        public int IdCuenta { get; set; }

        [Display(Name = "Código Cuenta", ShortName = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite Código de la Cuenta")]
        [StringLength(10, ErrorMessage = "Longitud máxima de la Cuenta debe ser de 10 caracteres", MinimumLength = 1)]
        public string CodigoCuenta { get; set; } = null!;

        [Display(Name = "Nombre Cuenta", ShortName = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite Nombre de la Cuenta")]
        [StringLength(100, ErrorMessage = "Longitud máxima del Nombre debe ser de 100 caracteres", MinimumLength = 1)]
        public string NombreCuenta { get; set; } = null!;

        [Display(Name = "Nivel Cuenta", ShortName = "Nivel")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione el Nivel de la Cuenta")]
        [StringLength(1, ErrorMessage = "Longitud máxima del Nivel debe ser de 1 caracteres", MinimumLength = 1)]
        public string Nivel { get; set; } = null!;

        [Display(Name = "Estimaciones Niif", ShortName = "Estimaciones")]
        public bool Estimaciones { get; set; }

        [Display(Name = "Tipo Base", ShortName = "Tipo Base")]
        [StringLength(1, ErrorMessage = "Longitud máxima del Tipo Base debe ser de 1 caracteres", MinimumLength = 1)]
        public string TipoBase { get; set; } = null!;

        [Display(Name = "Estado", ShortName = "Estado")]
        public bool Estado { get; set; }

        [Display(Name = "Empresa", ShortName = "empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite Empresa")]
        public int EmpresaId { get; set; }

        [NotMapped]
        public string CodigoNombreCuenta { get => CodigoCuenta!.Trim() + " - " + NombreCuenta!.Trim(); }

        [NotMapped]
        public string NombreEstado { get => Estado ? "Activo" : "Inactivo"; }
    }
}
