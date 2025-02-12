using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;
using Sales.Shared.Servicios;
using System.Text;

namespace Sales.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/cuentas")]

    public class CuentasController : ControllerBase
    {
        private readonly DataContext context;

        public CuentasController(DataContext _context)
        {
            context = _context;
        }

        [HttpPost("ListaCuentas")]
        public async Task<ActionResult<List<Cuenta>>> GetListaCuentas(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("ListaCuentas")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentas = await context.Cuentas.Where(a => a.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentas;
        }

        [HttpPost("ListaCuentasFiltrado/{filtro}")]
        public async Task<ActionResult<List<Cuenta>>> GetListaCuentasFiltrado(ServicioUsuario ServicioUsuario, string filtro)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("ListaCuentasFiltrado")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentas = await context.Cuentas.Where(a => a.EmpresaId == ServicioUsuario.Empresa && (a.NombreCuenta.Contains(filtro) || a.CodigoCuenta.Contains(filtro))).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentas;
        }

        [HttpPost("GetCuenta/{id}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(ServicioUsuario ServicioUsuario, int id)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("GetCuenta")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuenta = await context.Cuentas.FirstOrDefaultAsync(x => x.IdCuenta == id && x.EmpresaId == ServicioUsuario.Empresa);
            return cuenta != null ? cuenta : NotFound("Error!. No existe cuenta!!.");
        }

        [HttpGet("ExisteCuenta/{codigoCuenta}/{idempresa}")]
        public async Task<ActionResult<bool>> ExisteCuenta(string codigoCuenta, int idempresa)
        {
            bool existe = await context.Cuentas.AnyAsync(x => x.EmpresaId == idempresa & x.CodigoCuenta == codigoCuenta);
            return existe;
        }

        [HttpPost("CuentasAuxiliares/{nivel}")]
        public async Task<ActionResult<List<Cuenta>>> CuentasAuxiliares(ServicioUsuario ServicioUsuario, int nivel)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasAuxiliares")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasauxiliares = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) > nivel && c.Estimaciones == false && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentasauxiliares;
        }

        [HttpPost("CuentasAuxiliaresGeneral")]
        public async Task<ActionResult<List<Cuenta>>> CuentasAuxiliaresGeneral(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasAuxiliaresGeneral")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasauxiliares = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) > 1 && c.Estimaciones == false && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentasauxiliares;
        }

        [HttpPost("CuentasAuxiliaresFiltrado/{nivel}/{filtro}")]
        public async Task<ActionResult<List<Cuenta>>> GetCuentasAuxiliaresFiltrado(ServicioUsuario ServicioUsuario, int nivel, string filtro)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasAuxiliaresFiltrado")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasauxiliares = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) > nivel && c.Estimaciones == false && c.EmpresaId == ServicioUsuario.Empresa && (c.NombreCuenta.Contains(filtro) || c.CodigoCuenta.Contains(filtro))).OrderBy(x => x.CodigoCuenta).Take(50).ToListAsync();
            return cuentasauxiliares;
        }

        [HttpPost("CuentasAuxiliaresEstimaciones")]
        public async Task<ActionResult<List<Cuenta>>> CuentasAuxiliaresEstimaciones(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasAuxiliaresEstimaciones")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasauxiliares = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) > 1 && c.Estimaciones == true && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).Take(50).ToListAsync();
            return cuentasauxiliares;
        }

        [HttpPost("CuentasAuxiliaresFiltradoEstimaciones/{filtro}")]
        public async Task<ActionResult<List<Cuenta>>> GetCuentasAuxiliaresFiltradoEstimaciones(ServicioUsuario ServicioUsuario, string filtro)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasAuxiliaresFiltradoEstimaciones")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasauxiliares = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) > 1 && c.Estimaciones == true && c.EmpresaId == ServicioUsuario.Empresa && (c.NombreCuenta.Contains(filtro) || c.CodigoCuenta.Contains(filtro))).OrderBy(x => x.CodigoCuenta).Take(50).ToListAsync();
            return cuentasauxiliares;
        }

        [HttpPost("CuentasMayores")]
        public async Task<ActionResult<List<Cuenta>>> CuentasMayores(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasMayores")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasmayores = await context.Cuentas.Where(c => c.CodigoCuenta.Length == 1 && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentasmayores;
        }

        [HttpPost("CuentasPatrimonio/{nivel}")]
        public async Task<ActionResult<List<Cuenta>>> CuentasPatrimonio(ServicioUsuario ServicioUsuario, int nivel)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasPatrimonio")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasmayores = await context.Cuentas.Where(c => Convert.ToInt32(c.Nivel) == nivel && c.CodigoCuenta.Substring(0, 1) == "3" && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentasmayores;
        }

        [HttpPost("CuentasRetenciones")]
        public async Task<ActionResult<List<Cuenta>>> CuentasRetenciones(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasRetenciones")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentas = await context.Cuentas.Where(c => c.CodigoCuenta.Substring(0, 4) == "2365" || c.CodigoCuenta.Substring(0, 4) == "2367" || c.CodigoCuenta.Substring(0, 4) == "2368" && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentas;
        }

        [HttpPost("CuentasMayores2")]
        public async Task<ActionResult<List<Cuenta>>> CuentasMayores2(ServicioUsuario ServicioUsuario)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasMayores2")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentasmayores = await context.Cuentas.Where(c => c.Nivel == "1" && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            return cuentasmayores;
        }

        [HttpPost("CuentasCartera")]
        public async Task<ActionResult<List<Cuenta>>> CuentasCartera(ServicioUsuario ServicioUsuario)
        {
            var cuentascartera = await context.Cuentas.Where(c => (Convert.ToInt32(c.Nivel) == 4 || Convert.ToInt32(c.Nivel) == 6) && c.EmpresaId == ServicioUsuario.Empresa).OrderBy(x => x.CodigoCuenta).ToListAsync();
            if (cuentascartera != null)
            {
                return cuentascartera;
            }
            else
            {
                return BadRequest("Error!. No existen cuentas!!.");
            }
        }

        [HttpPost("CuentasCarteraFiltrado/{nivel}/{filtro}")]
        public async Task<ActionResult<List<Cuenta>>> GetCuentasCuentasCarteraFiltrado(ServicioUsuario ServicioUsuario, int nivel, string filtro)
        {
            if (ServicioUsuario.Servicio is null || !ServicioUsuario.Servicio.Equals("CuentasCarteraFiltrado")) { return BadRequest("Acceso no permitido a servicio"); }
            var cuentascartera = await context.Cuentas.Where(c => (Convert.ToInt32(c.Nivel) == 4 || Convert.ToInt32(c.Nivel) == 6) && c.EmpresaId == ServicioUsuario.Empresa && (c.NombreCuenta.Contains(filtro) || c.CodigoCuenta.Contains(filtro))).OrderBy(x => x.CodigoCuenta).Take(50).ToListAsync();
            return cuentascartera;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Cuenta Cuenta)
        {
            if (!ModelState.IsValid) { return UnprocessableEntity(ModelState); }
            var email = HttpContext.User.Identity.Name;
            context.Add(Cuenta);
            await context.SaveChangesAsync();
            //await logService.LogSave(JsonSerializer.Serialize(Cuenta), "Cuentas", "Creacion", Cuenta.IdCuenta, Cuenta.EmpresaId, email);
            return Cuenta.IdCuenta;
        }

        [HttpPost("CuentasBase")]
        [AllowAnonymous]
        public async Task<ActionResult> PostCuentasBase(Cuenta cuenta)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "MigracionInicial.sql");
            if (System.IO.File.Exists(path))
            {
                const Int32 BufferSize = 128;
                using (var fileStream = System.IO.File.OpenRead(path))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    string script = streamReader.ReadToEnd();
                    script = script.Replace("'@EMPRESAID'", cuenta.EmpresaId.ToString());
                    try
                    {
                        await context.Database.ExecuteSqlRawAsync(script);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        string x = ex.ToString();
                    }
                }
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Cuenta Cuenta)
        {
            if (!ModelState.IsValid) { return UnprocessableEntity(ModelState); }
            var email = HttpContext.User.Identity.Name;
            var cuentaDB = await context.Cuentas.FirstOrDefaultAsync(x => x.CodigoCuenta == Cuenta.CodigoCuenta && x.EmpresaId == Cuenta.EmpresaId);
            if (cuentaDB == null) { return NotFound(); }
            //cuentaDB = mapper.Map(Cuenta, cuentaDB);
            await context.SaveChangesAsync();
            //await logService.LogSave(JsonSerializer.Serialize(Cuenta), "Cuentas", "Modificación", Cuenta.IdCuenta, Cuenta.EmpresaId, email);
            return NoContent();
        }

        [HttpPut("EstadoCuenta")]
        public async Task<ActionResult> PutEstadoCuenta(Cuenta Cuenta)
        {
            if (!ModelState.IsValid) { return UnprocessableEntity(ModelState); }
            Cuenta.Estado = !Cuenta.Estado;
            var email = HttpContext.User.Identity.Name;
            var cuentaDB = await context.Cuentas.FirstOrDefaultAsync(x => x.IdCuenta == Cuenta.IdCuenta && x.EmpresaId == Cuenta.EmpresaId);
            if (cuentaDB is null) { return NotFound(); }
            //cuentaDB = mapper.Map(Cuenta, cuentaDB);
            await context.SaveChangesAsync();
            //await logService.LogSave(JsonSerializer.Serialize(Cuenta), "Cuentas", "Estado", Cuenta.IdCuenta, Cuenta.EmpresaId, email);
            return NoContent();
        }

        [HttpDelete("EliminaCuenta/{id}/{empresaid}")]
        public async Task<ActionResult> Delete(int id, int empresaid)
        {
            var email = HttpContext.User.Identity.Name;
            var cuenta = await context.Cuentas.AsNoTracking().FirstOrDefaultAsync(x => x.IdCuenta == id && x.EmpresaId == empresaid);
            if (cuenta is null) { return NotFound(); }
            //await logService.LogSave(JsonSerializer.Serialize(cuenta), "Cuentas", "Eliminación", id, empresaid, email);
            context.Remove(new Cuenta { IdCuenta = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpGet("ValidaEliminaCuenta/{id}/{empresaid}")]
        //public async Task<ActionResult<int>> ValidaEliminaCuenta(int id, int empresaid)
        //{
        //    int respuesta = 0;
        //    var movimiento = context.ContabilidadItems.FirstOrDefaultAsync(c => c.CuentaId == id && c.EmpresaId == empresaid).Result;
        //    if (movimiento != null)
        //    {
        //        respuesta = 1;
        //    }
        //    else
        //    {
        //        var cuenta = context.Cuentas.FirstOrDefaultAsync(c => c.IdCuenta == id && c.EmpresaId == empresaid).Result;
        //        var cuentashijos = await context.Cuentas.Where(c => c.CodigoCuenta.StartsWith(cuenta.CodigoCuenta.Trim()) && c.CodigoCuenta != cuenta.CodigoCuenta && c.EmpresaId == empresaid).ToListAsync();
        //        if (cuentashijos.Count > 0)
        //        {
        //            respuesta = 2;
        //        }
        //    }
        //    return respuesta;
        //}
    }
}
