using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TestTechSolve.Context;
using TestTechSolve.Modelos;
using TestTechSolve.Negocio;
using TestTechSolve.ViewModel;

namespace TestTechSolve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaArchivoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CargaArchivoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Cargar([FromForm] CargarArchivo datos)
        {
            try
            {
                if (datos.Archivo.Length > 0)
                {
                    CargaArchivoNegocio carga = new CargaArchivoNegocio(_context, datos);
                    carga.CargarArchivo();

                    return File(Encoding.UTF8.GetBytes(carga.Resultado), "text/plain", "Resultado.txt");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                return this.Content(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


     
}
}
