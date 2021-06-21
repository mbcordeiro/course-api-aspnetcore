using Course.Api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Business.Repositories
{
    interface IUserRepository
    {
        void Add(User user);
        void Save();
        User GetUser(string Login);
    }
}
