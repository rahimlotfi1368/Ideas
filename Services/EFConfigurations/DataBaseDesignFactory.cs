using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFConfigurations
{
    internal class DataBaseDesignFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
           
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseSqlServer("Persist Security Info=True;Initial Catalog=IdeasDataBase;User Id=Mrahim;Password=799368@bank;Source=.\\MSSQLSERVER2019;");
            //optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=IdeasDataBase;Data Source=DESKTOP-HHQAC3S\\SQL2019");
            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}//
