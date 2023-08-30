using RateSetter.Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateSetter.Service
{
    public interface IUserMatcher
    {
        bool IsMatch(User newUser, User existingUser);
    }
}
