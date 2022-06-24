namespace Vk.Model
{
    public class ParseWallPost
    {
        public int WallPostId { get; set; }
        public int GroupId { get; set; }
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
        public int RepostsCount { get; set; }
        public DateTime Date { get; set; }
        public string Uniqueid { get; set; } = Guid.NewGuid().ToString();
    }
}
