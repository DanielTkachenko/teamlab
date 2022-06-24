namespace Vk.Model
{
    public class ParseComment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; } 
        public int GroupId { get; set; }
        public int WallPostId { get; set; }
        public int LikesCount { get; set; }
        public DateTime Date { get; set; }
        public string Uniqueid { get; set; } = Guid.NewGuid().ToString();
    }
}
