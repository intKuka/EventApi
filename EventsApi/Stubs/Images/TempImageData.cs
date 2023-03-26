using SC.Internship.Common.Exceptions;

namespace EventsApi.Stubs.Images
{
    public class TempImageData
    {
        public static readonly List<EventImage> Images = new();


        public static List<EventImage> GetAll()
        {
            return Images;
        }

        public static EventImage GetById(Guid id)
        {
            var image = Images.FirstOrDefault(i => i.Id == id);
            return image ?? throw new ScException("Изображение не найдено");
        }
    }
}
