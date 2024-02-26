using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIPruebas.Models;

namespace APIPruebas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public readonly SeriesContext _dbContext;

        public UsuarioController(SeriesContext Context) { 
        
            _dbContext = Context;
        }
      

        [HttpGet]
        [Route("UsuariosList")]
        public IActionResult UsuariosList()
        {
            List<Usuario> seriesList = new List<Usuario>();
            try
            {
                seriesList = _dbContext.Usuarios.Include(c => c.oSeries).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = seriesList });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = seriesList });
            }
        }

        [HttpGet]
        [Route("UsuariosListOne/{id_Usuario:int}")]
        public IActionResult UsuariosListOne(int id_Usuario)
        {
            Usuario oUsuario = _dbContext.Usuarios.Find(id_Usuario);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }
            
            try
            {
                 oUsuario = _dbContext.Usuarios.Include(c => c.oSeries).Where(p => p.IdUsuario == id_Usuario).FirstOrDefault();

     

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = oUsuario });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oUsuario });
            }
        }




        [HttpPost]
        [Route("GuardarUsuario")]

        public IActionResult GuardarUsuario([FromBody] Usuario us)
        {

            try
            {
                _dbContext.Usuarios.Add(us);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }




        }

        [HttpPut]
        [Route("EditarUsuario")]

        public IActionResult EditarSeries([FromBody] Usuario us)
        {
            Usuario oUsuario = _dbContext.Usuarios.Find(us.IdUsuario);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {

                oUsuario.NombreUsuario = oUsuario.NombreUsuario is null ? oUsuario.NombreUsuario : us.NombreUsuario;
                oUsuario.NombreCompleto = oUsuario.NombreCompleto is null ? oUsuario.NombreCompleto : us.NombreCompleto;
                oUsuario.CorreoElectronico = oUsuario.CorreoElectronico is null ? oUsuario.CorreoElectronico : us.CorreoElectronico;
                oUsuario.FechaRegistro = oUsuario.FechaRegistro is null ? oUsuario.FechaRegistro : us.FechaRegistro;
                oUsuario.Activo = oUsuario.Activo is null ? oUsuario.Activo : us.Activo;

                _dbContext.Usuarios.Update(oUsuario);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }




        }

        [HttpDelete]
        [Route("EliminarUsuario/{id_Usuario:int}")]
        public IActionResult EliminarUsuario(int id_Usuario)
        {

            Usuario oUsuario = _dbContext.Usuarios.Find(id_Usuario);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {




                _dbContext.Usuarios.Remove(oUsuario);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }

        }


    }   
   
}

           

