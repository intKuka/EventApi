namespace UsersService
{
    public class UserData
    {
        public static List<User> Users = new()
        {
            new User() { Id = new Guid("4bf981b9-fdd5-4854-b438-af792493a221"), Nickname = "USER_1" },
            new User() { Id = new Guid("ae230ee7-a828-42c7-a3aa-9bce48f3fb28"), Nickname = "USER_2" },
            new User() { Id = new Guid("17f8eeb7-c8d0-409c-ad55-a3338b554cef"), Nickname = "USER_3" },
            new User() { Id = new Guid("13ada688-f7ca-419d-92ab-e4c6634b2b2d"), Nickname = "USER_4" },
            new User() { Id = new Guid("40de4786-aa6e-44ee-9c5a-5954f6b42711"), Nickname = "USER_5" },
        };

        public static List<User> GetAll()
        {
            return Users;
        }

        public static bool CheckUser(Guid id)
        {
            return Users.Any(user => user.Id == id);
        }
    }
}
