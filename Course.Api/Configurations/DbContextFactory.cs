using Course.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            var options = new DbContextOptionsBuilder<CourseDbContext>();
            options.UseSqlServer("server = localhost, database = courses; user=sa; password=sa;");
            var context = new CourseDbContext(options.Options);
            return context;
        }
    }
}
