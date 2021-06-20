using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Business.Entities
{
    public class CourseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCode { get; set; }
        public virtual User User { get; set; }
    }
}
