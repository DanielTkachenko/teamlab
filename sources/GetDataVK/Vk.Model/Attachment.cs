using Vk.Model.AttachmentTypes;

namespace Vk.Model
{
    public class Attachment
    {
        // Название типа вложения
        public string Type { get; set; }

        public Photo? Photo { get; set; }

        public Video? Video { get; set; }

        public Audio? Audio { get; set; }

        public Document? Document { get; set; }

        // Прикрепленная ссылка
        public Link? Link { get; set; }

        // Опрос
        public Poll? Poll { get; set; }

        // Вики-страница
        public Page? Page { get; set; }

        // Фотоальбом
        public Album? Album { get; set; }

        // Массив из строк, содержащих идентификаторы фотографий
        public IList<string>? PhotosList { get; set; }

        // Товар
        public Market? Market { get; set; }

        // Подборка товаров
        public MarketAlbum? MarketAlbum { get; set; }

        public Sticker? Sticker { get; set; }

        // Встреча (мероприятие)
        public Meeting? Meeting { get; set; }


    }
}
