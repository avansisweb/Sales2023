using Sales.Shared.Enums;

namespace Sales.Shared.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; } = null!;

        public DateTime Expiration { get; set; }

        public string? Email { get; set; }

        //public string? Nombres { get; set; }

        //public string? Apellidos { get; set; }

        //public string? TelefonoMovil { get; set; }

        //public int Documento { get; set; }

        public UserType TipoUsuario { get; set; }

        public string? Id { get; set; }

        public int EmpresaId { get; set; }

        //public int NombreId { get; set; }
    }
}
