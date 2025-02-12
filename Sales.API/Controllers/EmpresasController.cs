using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly DataContext _context;

        public EmpresasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            if (empresa == null)
            {
                return NotFound();
            }
            return Ok(empresa);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> PostAsync(Empresa empresa)
        //{
        //    _context.Add(empresa);
        //    await _context.SaveChangesAsync();
        //    return Ok(empresa);
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> PostAsync(Empresa empresa)
        {
            _context.Add(empresa);
            await _context.SaveChangesAsync();
            return Ok(empresa.IdEmpresa);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Empresa empresa)
        {
            _context.Update(empresa);
            await _context.SaveChangesAsync();
            return Ok(empresa);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var city = await _context.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            if (city == null)
            {
                return NotFound();
            }
            _context.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("ExisteEmpresa/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ExisteEmpresa(string email)
        {
            return await _context.Empresas.AnyAsync(x => x.Email == email);
        }
    }
}
