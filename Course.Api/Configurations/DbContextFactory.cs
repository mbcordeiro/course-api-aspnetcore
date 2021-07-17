using Course.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Configurations
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().Build();
            var options = new DbContextOptionsBuilder<CourseDbContext>();
            options.UseSqlServer(configurationBuilder.GetConnectionString(""));
            var context = new CourseDbContext(options.Options);
            return context;
        }
    }
}
