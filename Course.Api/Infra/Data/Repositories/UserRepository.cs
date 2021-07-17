using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> GetUser(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
