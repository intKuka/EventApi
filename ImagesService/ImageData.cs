namespace ImagesService
{
    public class ImageData
    {
        public readonly List<Image> Images = new()
        {
            new Image { Id= new Guid("4c8ebbeb-ffba-4851-8300-ffd192e99372"), Name="Image1" },
            new Image { Id= new Guid("e9b53393-0d73-4dda-8f18-4a23afbf9d05"), Name="Image2" },
            new Image { Id= new Guid("08d5bf95-9792-4642-bac0-c9e77eedafcf"), Name="Image3" },
        };

        private readonly ImageDeletionSender _deletionSender;

        public ImageData(ImageDeletionSender deletionSender)
        {
            _deletionSender = deletionSender;
        }

        public List<Image> GetAll()
        {
            return Images;
        }

        public bool CheckImage(Guid id)
        {
            return Images.Any(x => x.Id == id);
        }

        public async Task DeleteImage(Guid id)
        {
            var image = Images.FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                Images.Remove(image);
                _deletionSender.SendEvent(id);
                Console.WriteLine($"Изображение {id} удалено");

            }
            else Console.WriteLine($"Изображение {id} не найдено");

            await Task.CompletedTask;
        }
    }
}
