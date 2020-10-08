using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestTechSolve.Context;
using TestTechSolve.Modelos;
using TestTechSolve.Repositorio;
using TestTechSolve.ViewModel;

namespace TestTechSolve.Negocio
{
    public class CargaArchivoNegocio
    {
        public string Resultado { get; set; }
        private ApplicationContext Context { get; set; }
        private CargarArchivo CargaArchivo { get; set; }
        public CargaArchivoNegocio(ApplicationContext context, CargarArchivo data)
        {
            Context = context;
            CargaArchivo = data;
        }

        public void CargarArchivo()
        {
            List<int> resultado = new List<int>();
            using (var reader = new StreamReader(CargaArchivo.Archivo.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    resultado.Add(int.Parse(reader.ReadLine()));
            }

            ProcesarDatos(resultado);

            GuardarTraza();

        }

        private void GuardarTraza()
        {
            Traza traza = new Traza()
            {
                FechaTraza = DateTime.Now.ToLocalTime(),
                Identificacion = CargaArchivo.Cedula
            };

            using TrazaRepositorio trazaRepo = new TrazaRepositorio(Context);
            trazaRepo.GuardarTraza(traza);
        }

        private void ProcesarDatos(List<int> datos)
        {
            int dias = datos[0];
            if (dias<1 || dias >500)
            {
                throw new ArgumentException("Los días deben ser mayores a 0 y menor o igual a 500");
            }
            if (datos.Any(x => x < 1 || x > 100))
            {
                throw new ArgumentException("El peso de los elementos debe ser mayor a 0 y menor o igual a 100");

            }
            int diaActual = 1;
            for (int i = 1; i < datos.Count; i++)
            {
                string caso = $"Case # {diaActual}: {ValidarCasos(i, datos)}";
                Resultado = string.Concat(Resultado, caso, Environment.NewLine);
                i += datos[i];
                diaActual++;
            }
        }

        private int ValidarCasos(int i, List<int> datos)
        {
            int elementosCargar = datos[i];
            if (elementosCargar<1 || elementosCargar>100)
            {
                throw new ArgumentException("Los elementos a cargar por días deben ser mayores a 0 y y menores o iguales a 100");
            }
            List<int> pesosElementosDia = new List<int>();
            for (int j = i + 1; j < i + 1 + datos[i]; j++)
            {
                pesosElementosDia.Add(datos[j]);
            }

            return ValidarCasos(pesosElementosDia);
        }

        private int ValidarCasos(List<int> elementos)
        {
            int mayor = elementos.Max();
            elementos.Remove(mayor);

            int peso = 0;
            int i = 1;
            int viajes = 0;

            while (peso < 50 && mayor < 50)
            {
                if (elementos.Count == 0)
                {
                    return 0;
                }

                int menor = elementos.Min();
                elementos.Remove(menor);
                i++;
                peso = mayor * i;
            }

            viajes++;

            if (elementos.Count > 0)
            {
                viajes += ValidarCasos(elementos);
            }

            return viajes;
        }

    }
}
