using System.Web;
using Newtonsoft.Json;
using TeamlabLogger.Services;
using Vk.Model;
using Vk.Model.ResponseTypes;

namespace Vk.Provider
{
    public class VkProvider
    {

        private static string accessToken = "0f8082fe0f8082fe0f8082fe140ffce25300f800f8082fe6de9bd283e8906f204ad62db";

        private static string apiVersion = "5.131";

        static readonly HttpClient client = new HttpClient();
        private readonly LoggerService<VkProvider> _logger = new LoggerService<VkProvider>();

        // Метод преобразования формата времени unix -> DateTime
        public static DateTime UnixTimeToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }


        public static Task<HttpResponseMessage> VkGet(string method, Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder($"https://api.vk.com/method/{method}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_token"] = accessToken;
            query["v"] = apiVersion;

            foreach (var key in parameters.Keys)
            {
                query[key] = parameters[key];
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            return client.GetAsync(url);
        }


        public async Task<List<ParseGroup>> GetGroupsInfo(List<int> groupIds)
        {
            try
            {
                string strGroupIds = string.Join(",", groupIds);
                HttpResponseMessage response = await VkGet("groups.getById", new Dictionary<string, string>
                {
                    ["fields"] = "members_count",
                    ["group_ids"] = strGroupIds
                });

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}. " +
                        $"Информация о группах успешно загружена");
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<VkListResponse<Group>>(content);

                    List<ParseGroup> groups = new();
                    foreach (var item in deserializedContent.Response)
                    {
                        ParseGroup group = new();
                        group.GroupId = item.Id;
                        group.Name = item.Name;
                        group.ScreenName = item.Screen_name;
                        group.MembersCount = item.Members_Count;
                        groups.Add(group);
                    }

                    return groups;
                }
                throw new ArgumentException($"Ошибка при загрузке данных" +
                    $"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }


        public async Task<(List<ParseUser>, List<ParseUsersInGroups>)> GetGroupMembers(int groupId, int count, int offset)
        {
            try
            {
                HttpResponseMessage response = await VkGet("groups.getMembers", new Dictionary<string, string>
                {
                    ["group_id"] = $"{groupId}",
                    ["count"] = $"{count}",
                    ["offset"] = $"{offset}",
                    ["fields"] = "photo_100"
                });

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}. " +
                        $"Подписчики успешно загружены");
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<VkItemsResponse<User>>(content);

                    List<ParseUser> users = new();
                    List<ParseUsersInGroups> listUsersInGroups = new();
                    foreach (var item in deserializedContent.Response.Items)
                    {
                        ParseUsersInGroups usersInGroups = new();
                        ParseUser user = new();
                        user.UserId = item.Id;
                        usersInGroups.UserId = item.Id;
                        usersInGroups.GroupId = groupId;
                        users.Add(user);
                        listUsersInGroups.Add(usersInGroups);
                    }

                    return (users, listUsersInGroups);
                }
                throw new ArgumentException($"Ошибка при загрузке данных" +
                    $"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return (null!, null!);
            }
        }


        public async Task<List<ParseWallPost>> WallGet(int groupId, int count)
        {
            try
            {
                HttpResponseMessage response = await VkGet("wall.get", new Dictionary<string, string>
                {
                    ["owner_id"] = $"-{groupId}",
                    ["count"] = $"{count}",
                });

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}. " +
                        $"Посты успешно загружены");
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<VkItemsResponse<WallPost>>(content);

                    List<ParseWallPost> posts = new();
                    foreach (var item in deserializedContent.Response.Items)
                    {
                        ParseWallPost post = new();
                        post.WallPostId = item.Id;
                        post.GroupId = groupId;
                        if (item.Views == null) post.ViewsCount = 0;
                        else post.ViewsCount = item.Views.Count;
                        post.LikesCount = item.Likes.Count;
                        post.RepostsCount = item.Reposts.Count;
                        post.Date = UnixTimeToDateTime(item.Date);
                        posts.Add(post);
                    }
                    return posts;
                }
                throw new ArgumentException($"Ошибка при загрузке данных" +
                    $"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }

        }


        public async Task<List<ParseComment>> GetComments(int groupId, int postId, int count)
        {
            try
            {
                HttpResponseMessage response = await VkGet("wall.getComments", new Dictionary<string, string>
                {
                    ["owner_id"] = $"-{groupId}",
                    ["post_id"] = $"{postId}",
                    ["need_likes"] = "1",
                    ["count"] = $"{count}"
                });

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}. " +
                        $"Коммментарии успешно загружены");
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<VkItemsResponse<Comment>>(content);

                    List<ParseComment> comments = new();
                    if (deserializedContent.Response != null)
                    {
                        foreach (var item in deserializedContent.Response.Items)
                        {
                            ParseComment comment = new();
                            comment.CommentId = item.Id;
                            if (item.From_id > 0)
                            {
                                comment.UserId = item.From_id;
                                comment.GroupId = groupId;
                            }
                            else continue;

                            comment.WallPostId = postId;
                            if (item.Likes != null)
                                comment.LikesCount = item.Likes.Count;
                            comment.Date = UnixTimeToDateTime(item.Date);
                            comments.Add(comment);
                        };
                    }
                    return comments;
                }
                throw new ArgumentException($"Ошибка при загрузке данных" +
                    $"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }


        public async Task<List<ParseLike>> GetWallPostLikes(int groupId, int postId, int count)
        {
            try
            {
                HttpResponseMessage response = await VkGet("likes.getList", new Dictionary<string, string>
                {
                    ["type"] = "post",
                    ["owner_id"] = $"-{groupId}",
                    ["item_id"] = $"{postId}",
                    ["count"] = $"{count}",
                });

                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}. " +
                        $"Лайки под постами успешно загружены");
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<VkItemsResponse<int>>(content);

                    List<ParseLike> postLikes = new();

                    if (deserializedContent.Response != null)
                    {
                        foreach (int item in deserializedContent.Response.Items)
                        {
                            ParseLike like = new();
                            like.UserId = item;
                            like.WallPostId = postId;
                            postLikes.Add(like);
                        };
                    }
                    return postLikes;
                }
                throw new ArgumentException($"Ошибка при загрузке данных" +
                    $"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }
    }
}