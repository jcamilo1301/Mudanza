using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading;
using TestTechSolve.Context;
using TestTechSolve.Controllers;
using TestTechSolve.PruebasUnitarias;
using TestTechSolve.ViewModel;

namespace UnitTest
{
    [TestClass]

    public class CargarArchivoTest
    {
        private ApplicationContext _dbContext;
        [TestMethod]
        public void CargarArchivo()
        {
            _dbContext = new Contexto().GetAplicattionContext();

            CargaArchivoController controlador = new CargaArchivoController(_dbContext);

            var fileMock = new Mock<IFormFile>();

            var content = "1\r\n3\r\n20\r\n20\r\n20";

            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            CargarArchivo carga = new CargarArchivo() { Archivo = fileMock.Object, Cedula = "1234" };

           var respuesta= controlador.Cargar(carga);
            var actual = respuesta as FileContentResult;
            Assert.IsNotNull(actual);
        }
    }
}
