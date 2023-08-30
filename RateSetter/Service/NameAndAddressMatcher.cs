using RateSetter.Enitity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RateSetter.Service
{
    public class NameAndAddressMatcher : IUserMatcher
    {
        public bool IsMatch(User newUser, User existingUser)
        {
            return (CompareNames(newUser.Name, newUser.Name) &&
                CompareAddresses(CombinedAddress(newUser), CombinedAddress(existingUser)));
        }

        private string CombinedAddress(User user)
        {
            return user.Address.StreetAddress + "" + user.Address.Suburb + " " + user.Address.State;
        }

        private bool CompareNames(string newUserName, string existingUserName)
        {
            return string.Equals(newUserName.ToLower(), existingUserName.ToLower(), StringComparison.OrdinalIgnoreCase);
        }

        private bool CompareAddresses(string newUserAddress, string existingUserAddress)
        {
            string processedAddress1 = RemoveUnusualCharacters(newUserAddress);
            string processedAddress2 = RemoveUnusualCharacters(existingUserAddress);

            return string.Equals(processedAddress1.ToLower(), processedAddress2.ToLower(), StringComparison.OrdinalIgnoreCase);
        }

        private string RemoveUnusualCharacters(string input)
        {
            // Remove non-alphanumeric characters and whitespace
            return Regex.Replace(input, "[^a-zA-Z0-9]", string.Empty);
        }
    }
}
