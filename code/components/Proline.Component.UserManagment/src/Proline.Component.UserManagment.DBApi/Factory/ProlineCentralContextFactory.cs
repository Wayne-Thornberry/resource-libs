using Microsoft.EntityFrameworkCore.Design;
using Proline.CentralEngine.DBApi.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proline.Component.Engine.DBApi.Factory
{
    public class ProlineCentralContextFactory : IDesignTimeDbContextFactory<ProlineCentralContext>
    { 

        public ProlineCentralContext CreateDbContext(string[] args)
        {
            return new ProlineCentralContext("Server=(localdb)\\mssqllocaldb;Database=EF6MVCCore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
