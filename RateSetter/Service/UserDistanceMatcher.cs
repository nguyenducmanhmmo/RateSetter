using RateSetter.Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateSetter.Service
{
    public class UserDistanceMatcher : IUserMatcher
    {
        private const double EarthRadiusKm = 6371.0;
        private const double NearDistance = 6371.0;

        public bool IsMatch(User newUser, User existingUser)
        {
            return CalculateDistance(newUser, existingUser) <= NearDistance;
        }

        private double CalculateDistance(User newUser, User existingUser)
        {
            double lat1 = Math.PI * (double)newUser.Address.Latitude / 180.0;
            double lon1 = Math.PI * (double)newUser.Address.Longitude / 180.0;
            double lat2 = Math.PI * (double)existingUser.Address.Latitude / 180.0;
            double lon2 = Math.PI * (double)existingUser.Address.Longitude / 180.0;

            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dlon / 2) * Math.Sin(dlon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;

            return distance * 1000; // Convert to meters
        }
    }
}
