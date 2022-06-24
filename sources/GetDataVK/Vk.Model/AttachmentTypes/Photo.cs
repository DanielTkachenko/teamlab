namespace Vk.Model.AttachmentTypes
{
    // Размеры изображения с соотвествующими ссылками
    public class PhotoSize
    {
        // Размер изображения
        public string Type { get; set; }

        public string Url { get; set; } 

    }

    public class Photo
    {
        public int Id { get; set; }
        public List<PhotoSize> Sizes { get; set; }
    }
}
