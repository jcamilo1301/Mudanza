using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTechSolve.ViewModel
{
    public class CargarArchivo
    {
        public string Cedula { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
