using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Context
{
    public class SmartCityContextFactory : IDesignTimeDbContextFactory<SmartCityContext>
    {
        public SmartCityContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SmartCityContext>();
            builder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=SmartCity;User Id=sa;Password=smart;MultipleActiveResultSets=true");

            return new SmartCityContext(builder.Options);
        }
    }
}
