using TeamlabLogger.Services;
using TeamlabTask;
using Vk.Model;

namespace Downloader
{
    public class Download : IRunable
    {
        public List<int> GroupIds { get; set; } = new();
        public int WallCount { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool Group { get; set; }
        public bool User { get; set; }
        public bool Wall { get; set; }
        public bool Comments { get; set; }
        public bool Likes { get; set; }
        private readonly LoggerService<Download> _logger = new LoggerService<Download>();

        public async Task Run(Dictionary<string, string> arg)
        {
            WallCount = Int32.Parse(arg["WallCount"]);
            CommentCount = Int32.Parse(arg["CommentCount"]);
            LikeCount = Int32.Parse(arg["LikeCount"]);
            List<string> GroupIdsString = arg["GroupIds"].Split(',').ToList();
            foreach (var groupIdStr in GroupIdsString)
            {
                GroupIds.Add(Int32.Parse(groupIdStr));
            }
            List<ParseWallPost> vkPosts = new();
            List<ParseGroup> vkGroups = new();
            Resolver resolver = new();

            if (arg["Group"] == "true")
            {
                _logger.Info("Начинаю грузить группы...");
                vkGroups = resolver.GroupResolve(GroupIds).Result;
            }

            if (arg["User"] == "true") 
            {
                _logger.Info("Начинаю грузить подписчиков...");
                int count = 500;
                foreach (var vkGroup in vkGroups)
                {
                    for (int offset = 0; offset < vkGroup.MembersCount+count; offset += count)
                        await resolver.UserResolve(vkGroup.GroupId, count, offset);
                }
            }

            if (arg["Wall"] == "true") 
            {
                _logger.Info("Начинаю грузить посты...");
                foreach (int groupId in GroupIds)
                {
                    Thread.Sleep(1000);
                    vkPosts = resolver.WallPostResolve(groupId, WallCount).Result;
                    if (arg["Comments"] == "true")
                    {
                        _logger.Info("Начинаю грузить комментарии...");
                        foreach (var post in vkPosts)
                        {
                            await resolver.CommentResolve(groupId, post.WallPostId, CommentCount);
                        }
                    }

                    if (arg["Likes"] == "true")
                    {
                        _logger.Info("Начинаю грузить лайки под постами...");
                        foreach (var post in vkPosts)
                        {
                            await resolver.LikeResolve(groupId, post.WallPostId, LikeCount);
                        }
                    }
                }
            }
        }
    }
}
