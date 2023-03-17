using EventsApi.Features.Models;
using SC.Internship.Common.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace EventsApi.Features.Spaces
{
    public class TempSpaceData
    {
        public static readonly List<UserSpace> Spaces = new()
        {
            new UserSpace { Id=Guid.NewGuid(), Name="Space1" },
            new UserSpace { Id=Guid.NewGuid(), Name="Space2" },
            new UserSpace { Id=Guid.NewGuid(), Name="Space3" },
        };

        public static List<UserSpace> GetAll()
        {
            return Spaces;
        }

        public static UserSpace GetById(Guid id)
        {
            var space = Spaces.First(s => s.Id == id);
            return space ?? throw new ScException("Пространство не найдено");
        }
    }
}
