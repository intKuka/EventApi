using SC.Internship.Common.Exceptions;

namespace EventsApi.Stubs.Images
{
    public class TempImageData
    {
        public static readonly List<EventImage> Images = new()
        {
            new EventImage { Id= new Guid("4c8ebbeb-ffba-4851-8300-ffd192e99372"), Name="Image1" },
            new EventImage { Id= new Guid("e9b53393-0d73-4dda-8f18-4a23afbf9d05"), Name="Image2" },
            new EventImage { Id= new Guid("08d5bf95-9792-4642-bac0-c9e77eedafcf"), Name="Image3" },
        };


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
