using Vk.Model;

namespace Vk.Model.Interfaces
{
    public interface IVkProvider
    {
        /// <summary>
        /// Метод для получения информации о сообществе
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<List<Group>> GetGroupInfo(int groupId);

        /// <summary>
        /// Метод для получения списка подписчиков сообщества
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<User>> GetGroupMembers(int groupId, int count);

        /// <summary>
        /// Метод для получения списка записей со стены сообщества
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<WallPost>> WallGet(int groupId, int count);

        /// <summary>
        /// Метод для получения списка комментариев к записи на стене
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="postId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<Comment>> GetComments(int groupId, int postId, int offset, int count);

        /// <summary>
        /// Метод для получения списка идентификаторов пользователей,
        /// которые лайкнули запись на стене
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="postId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<Like>> GetWallPostLikes(int groupId, int postId, int offset, int count);
    }
}
