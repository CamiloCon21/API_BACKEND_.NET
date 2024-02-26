using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIPruebas.Models;

namespace APIPruebas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {

        public readonly SeriesContext _dbContext;

        public SeriesController(SeriesContext Context) { 
        
            _dbContext = Context;
        }
        [HttpGet]
        [Route("SeriesList")]
        public IActionResult SeriesList()
        {
            List <Series> seriesList = new List<Series> ();
            try
            {
                seriesList = _dbContext.Series.Include(c => c.oUsuarios).ToList();

                return StatusCode(StatusCodes.Status200OK, new {mensaje="OK", response = seriesList});

            }catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = seriesList });
            }
        }

        

        [HttpGet]
        [Route("SeriesListOne/{id_Serie:int}")]
        public IActionResult SeriesListOne(int id_Serie)
        {
            Series oSerie = _dbContext.Series.Find(id_Serie);

            if (oSerie == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oSerie = _dbContext.Series.Include(c => c.oUsuarios).Where(p => p.IdSerie == id_Serie ).FirstOrDefault();

                Console.WriteLine(oSerie + "ASKASJAS");

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = oSerie });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oSerie });
            }
        }

        [HttpPost]
        [Route("GuardarSeries")]

        public IActionResult GuardarSeries([FromBody] Series ser)
        {

            try
            {
                _dbContext.Series.Add(ser);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }




        }

        [HttpPut]
        [Route("EditarSeries")]

        public IActionResult EditarSeries([FromBody] Series ser)
        {
            Series oSerie = _dbContext.Series.Find(ser.IdSerie);

            if (oSerie == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {

                oSerie.Nombre = ser.Nombre is null ? oSerie.Nombre : ser.Nombre;
                oSerie.Genero = ser.Genero is null ? oSerie.Genero : ser.Genero;
                oSerie.AnioEstreno = ser.AnioEstreno is null ? oSerie.AnioEstreno : ser.AnioEstreno;
                oSerie.NumeroTemporadas = ser.NumeroTemporadas is null ? oSerie.NumeroTemporadas : ser.NumeroTemporadas;
                oSerie.Estado = ser.Estado is null ? oSerie.Estado : ser.Estado;
                oSerie.Descripcion = ser.Descripcion is null ? oSerie.Descripcion : ser.Descripcion;



                _dbContext.Series.Update(oSerie);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }




        }

        [HttpDelete]
        [Route("EliminarSerie/{id_Serie:int}")]
        public IActionResult EliminarSerie(int id_Serie)
        {

            Series oSerie = _dbContext.Series.Find(id_Serie);

            if (oSerie == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {




                _dbContext.Series.Remove(oSerie);
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

           

