namespace EventsApi.Features.Events.Data
{
    public class TempImageData
    {
        public static readonly List<EventImage> images = new()
        {
            new EventImage { Id=Guid.NewGuid(), Name="Image1" },
            new EventImage { Id=Guid.NewGuid(), Name="Image2" },
            new EventImage { Id=Guid.NewGuid(), Name="Image3" },
        };


        public static bool CheckImage(Guid id)
        {
            if (images.FirstOrDefault(i => i.Id == id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
