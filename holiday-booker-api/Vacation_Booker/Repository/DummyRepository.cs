using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class DummyRepository: IDummyRepository
    {
        private MyContext dc;
        public DummyRepository(MyContext T)
        {
            dc = T;
        }
        public List<User> getUsers()
        {
                return dc.Users.ToList();
        }
        public List<Resort> getResort()
        {
            return dc.Resorts.ToList();
        }

        public bool addUser(string username, string password)
        {
                //dc.Users.Add(new User() { Username = username, Password = password });
                //dc.SaveChanges();
                return true;
        }
    }
}
