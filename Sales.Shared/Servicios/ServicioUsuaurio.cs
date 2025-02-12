using Sales.Shared.Enums;

namespace Sales.Shared.Servicios
{
    public class ServicioUsuario
    {
        private bool _loggedIn;
        public string? Email { get; set; }
        //public string? Nombres { get; set; }
        //public string? Apellidos { get; set; }
        //public string? TelefonoMovil { get; set; }
        //public int Documento { get; set; }
        public UserType TipoUsuario { get; set; }
        public int Empresa { get; set; }
        public string? Servicio { get; set; }
        public string? UsuarioId { get; set; }
        //public List<parametro>? Parametros { get; set; }

        public event Action? OnChange;

        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
    //public class parametro
    //{
    //    public string? nombre;
    //    public string? valor;
    //}
}
