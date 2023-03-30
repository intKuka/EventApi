namespace SpacesService
{
    public class SpaceData
    {
        public readonly List<Space> Spaces = new()
        {
            new Space { Id=new Guid("169a4f10-0914-4d8d-b922-3958621a72a5"), Name="Space1" },
            new Space { Id=new Guid("9ddc1a21-d8d0-40a3-bd27-177bada48e97"), Name="Space2" },
            new Space { Id=new Guid("c14a6de5-fcdd-4ca6-aec8-2ff75a7cc2e1"), Name="Space3" },
        };

        private readonly SpaceDeletionSender _deletionSender;

        public SpaceData(SpaceDeletionSender deletionSender)
        {
            _deletionSender = deletionSender;
        }
        
        public List<Space> GetAll()
        {
            return Spaces;
        }
        
        public bool CheckSpace(Guid id)
        {
            return Spaces.Any(x => x.Id == id);
        }

        public async Task DeleteSpace(Guid id)
        {
            var space = Spaces.FirstOrDefault(x => x.Id == id);
            if (space != null)
            {
                Spaces.Remove(space);
                _deletionSender.SendEvent(id);
                Console.WriteLine($"Пространство {id} удалено");
            }
            else Console.WriteLine($"Пространство {id} не найдено");

            await Task.CompletedTask;
        }
    }
}
