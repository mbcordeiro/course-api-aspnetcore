using Course.Api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Business.Repositories
{
    interface ICourseRepository
    {
        void Add(CourseEntity curso);
        IList<CourseEntity> GetByUser(int userCode);
        void Save();
    }
}
