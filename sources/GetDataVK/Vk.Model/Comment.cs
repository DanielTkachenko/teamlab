namespace Vk.Model
{
    public class Comment
    {
        public int Id { get; set; }

        // Идентификатор автора комментария
        public int From_id { get; set; }

        // Идентификатор автора комментария (если это пользователь)
        public int? UserId { get; set; } 

        // Идентификатор автора комментария (если это сообщество)
        public int? GroupId { get; set; }

        public int WallPostId { get; set; }

        public int Date { get; set; }

        public string? Text { get; set; }

        public int? LikesCount { get; set; }

        public ActivityCounter? Likes { get; set; }

        // Список вложений
        public List<Attachment>? Attachments { get; set; }

    }
}
