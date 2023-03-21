using SC.Internship.Common.Exceptions;

namespace EventsApi.Stubs.Spaces
{
    public class TempSpaceData
    {
        public static readonly List<UserSpace> Spaces = new()
        {
            new UserSpace { Id=new Guid("169a4f10-0914-4d8d-b922-3958621a72a5"), Name="Space1" },
            new UserSpace { Id=new Guid("9ddc1a21-d8d0-40a3-bd27-177bada48e97"), Name="Space2" },
            new UserSpace { Id=new Guid("c14a6de5-fcdd-4ca6-aec8-2ff75a7cc2e1"), Name="Space3" },
        };

        public static List<UserSpace> GetAll()
        {
            return Spaces;
        }

        public static UserSpace GetById(Guid id)
        {
            var space = Spaces.FirstOrDefault(s => s.Id == id);
            return space ?? throw new ScException("Пространство не найдено");
        }
    }
}
