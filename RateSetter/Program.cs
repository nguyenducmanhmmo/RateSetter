// See https://aka.ms/new-console-template for more information

using RateSetter.Enitity;
using RateSetter.Service;

User newUser1 = new User { Name = "John Doe", ReferralCode = "ABC123", Address = new Address { State = "Sydney", Suburb = "NSW 2000", StreetAddress= "Level 3", Latitude = 10, Longitude = 20 } };
User newUser2 = new User { Name = "Manh Nguyen", ReferralCode = "XYZ456", Address = new Address { State = "Cau Giay", Suburb = "Bac Ninh", StreetAddress = "457", Latitude = 500, Longitude = 900 } };

List<IUserMatcher> userMatchers = new List<IUserMatcher>
        {
            new NameAndAddressMatcher(),
            new UserDistanceMatcher(),
            new UserDistanceMatcher()
            // Add more user matchers...
        };

bool isRejected = await RunUserMatchersAsync(newUser1, userMatchers);

if (isRejected)
{
    Console.WriteLine("New user registration is rejected due to matching rule.");
}
else
{
    Console.WriteLine("New user registration is accepted.");
}
    

    static async Task<bool> RunUserMatchersAsync(User newUser, List<IUserMatcher> userMatchers)
{
    List<Task<bool>> tasks = new List<Task<bool>>();
    List<User> existingUsers = new List<User>
        {
            new User { Name = "John Doe", ReferralCode = "ABC123",  Address = new Address { State = "Sydney", Suburb = "NSW 2000", StreetAddress= "Level 3", Latitude = 10, Longitude = 20 } },
            new User { Name = "Jane Smith", ReferralCode = "AB21C3", Address = new Address { State = "Ha Noi", Suburb = "Ha Dong", StreetAddress= "Quang Trung", Latitude = 10000, Longitude = 20000 }}
            // Add more existing users...
        };

    foreach (var existingUser in existingUsers)
        tasks.AddRange(
        userMatchers.Select(userMatcher =>
        Task.Run(() => userMatcher.IsMatch(newUser, existingUser))).ToList());

    bool[] results = await Task.WhenAll(tasks);

    return results.Any(result => result); // Return true if any of the user matchers return true
}
