using static System.Net.Mime.MediaTypeNames;

namespace EventsApi.Features.Events.Data
{
    public class TempSpaceData
    {
        public static readonly List<UserSpace> spaces = new()
        {
            new UserSpace { Id=Guid.NewGuid(), Name="Space1" },
            new UserSpace { Id=Guid.NewGuid(), Name="Space2" },
            new UserSpace { Id=Guid.NewGuid(), Name="Space3" },
        };

        public static List<UserSpace> GetAll()
        {
            return spaces;
        }

        public static UserSpace? GetById(Guid id)
        {
            return spaces.FirstOrDefault(e => e.Id == id);
        }

        public static bool CheckSpace(Guid id)
        {
            if (spaces.FirstOrDefault(s => s.Id == id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
