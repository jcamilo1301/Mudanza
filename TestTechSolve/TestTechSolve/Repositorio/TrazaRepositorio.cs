using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTechSolve.Context;
using TestTechSolve.Modelos;

namespace TestTechSolve.Repositorio
{
    public class TrazaRepositorio : IDisposable
    {
        public ApplicationContext Context { get; set; }

        public TrazaRepositorio(ApplicationContext context)
        {
            Context = context;
        }
        public void GuardarTraza(Traza traza)
        {
            Context.Traza.Add(traza);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)Context).Dispose();
        }
    }
}
