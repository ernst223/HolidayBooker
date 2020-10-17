using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    interface IDummyRepository
    {
        List<User> getUsers();
        bool addUser(string username, string password);
    }
}
