using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateSetter.Enitity
{
    public class User
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public string ReferralCode { get; set; }
    }
}
