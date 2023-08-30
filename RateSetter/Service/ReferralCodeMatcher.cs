using RateSetter.Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateSetter.Service
{
    public class ReferralCodeMatcher : IUserMatcher
    {
        public bool IsMatch(User newUser, User existingUser)
        {
            return ReferralCodesMatch(newUser.ReferralCode, existingUser.ReferralCode);
        }

        private bool ReferralCodesMatch(string newUserReferralCode, string existingUserReferralCode)
        {
            if (newUserReferralCode.Length != existingUserReferralCode.Length)
            {
                return false;
            }

            // Compare Referral Codes as they are
            if (string.Equals(newUserReferralCode, existingUserReferralCode, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Reverse the first 3 characters of new user referral code and compare
            if (newUserReferralCode.Length >= 3)
            {
                string reversedNewUserReferralCode = newUserReferralCode.Substring(0, 3) + new string(newUserReferralCode.Substring(0, 3).ToCharArray().Reverse().ToArray()) + newUserReferralCode.Substring(3);
                if (string.Equals(reversedNewUserReferralCode, existingUserReferralCode, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            // Reverse the first 3 characters of existing user referral code and compare
            if (existingUserReferralCode.Length >= 3)
            {
                string reversedExistingUserReferralCode = existingUserReferralCode.Substring(0, 3) + new string(existingUserReferralCode.Substring(0, 3).ToCharArray().Reverse().ToArray()) + existingUserReferralCode.Substring(3);
                if (string.Equals(newUserReferralCode, reversedExistingUserReferralCode, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
