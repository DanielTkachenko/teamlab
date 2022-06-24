namespace Vk.Model
{
    public class WallPost
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int Date { get; set; }

        public string? Text { get; set; }

        public int ViewsCount { get; set; }

        public int RepostsCount { get; set; }

        public int LikesCount { get; set; }

        public ActivityCounter Views { get; set; }

        public ActivityCounter Reposts { get; set; }

        public ActivityCounter Likes { get; set; }

        public List<Attachment>? Attachments { get; set; }

    }
}
