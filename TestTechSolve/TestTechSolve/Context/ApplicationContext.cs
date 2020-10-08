using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTechSolve.Modelos;

namespace TestTechSolve.Context
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options)
         : base(options)
        {
        }

        public DbSet<Traza> Traza { get; set; }
    }

   
}
