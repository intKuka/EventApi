using EventsApi.Features.Models;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Users
{
    public class TempUserData
    {
        internal static List<User> Users = new()
        {
            new User() { Id = Guid.NewGuid(), Nikname = "USER_1" },
            new User() { Id = Guid.NewGuid(), Nikname = "USER_2" },
            new User() { Id = Guid.NewGuid(), Nikname = "USER_3" },
            new User() { Id = Guid.NewGuid(), Nikname = "USER_4" },
            new User() { Id = Guid.NewGuid(), Nikname = "USER_5" },
        };

        public static List<User> GetAll()
        {
            return Users;
        }

        public static User GetById(Guid id)
        {
            var user = Users.FirstOrDefault(e => e.Id == id);
            return user ?? throw new ScException("Пользователь не найден");
        }
    }
}
