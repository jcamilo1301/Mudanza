using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTechSolve.Context;

namespace TestTechSolve.PruebasUnitarias
{

    public class Contexto
    {
        public ApplicationContext GetAplicattionContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .Options;
            var dbContext = new ApplicationContext(options);

            return dbContext;
        }
    }
}
