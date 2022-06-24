using Vk.Model;

namespace Downloader
{
    public interface IResolver
    {
        public Task<List<ParseGroup>> GroupResolve(List<int> groupIds);

        public Task UserResolve(int groupId, int count, int offset);

        public Task<List<ParseWallPost>> WallPostResolve(int groupId, int count);

        public Task CommentResolve(int groupId, int postId, int count);

        public Task LikeResolve(int groupId, int postId, int count);
    }
}
