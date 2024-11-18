using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Interfaces
{
    public interface IUserService
    {
        public User GetUserProfile();
    }
}
