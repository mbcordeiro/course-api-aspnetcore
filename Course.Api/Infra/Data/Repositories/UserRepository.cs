using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;

        public UserRepository(CourseDbContext context)
        {
            this._context = context;
        }

        public  void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public User GetUser(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
    }
}
