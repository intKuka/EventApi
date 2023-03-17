using EventsApi.Features.Models;
using Microsoft.Extensions.Logging;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Images
{
    public class TempImageData
    {
        public static readonly List<EventImage> Images = new()
        {
            new EventImage { Id=Guid.NewGuid(), Name="Image1" },
            new EventImage { Id=Guid.NewGuid(), Name="Image2" },
            new EventImage { Id=Guid.NewGuid(), Name="Image3" },
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
