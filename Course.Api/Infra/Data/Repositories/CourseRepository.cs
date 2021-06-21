using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Infra.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;

        public CourseRepository(CourseDbContext context)
        {
            this._context = context;
        }

        public void Add(CourseEntity curso)
        {
            _context.Courses.Add(curso);
        }

        public IList<CourseEntity> GetByUser(int userCode)
        {
            return _context.Courses
               .Include(i => i.User)
               .Where(c => c.UserCode == userCode)
               .ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
