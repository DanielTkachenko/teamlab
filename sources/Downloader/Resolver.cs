using TeamlabLogger.Services;
using ViQube.Model;
using ViQube.Provider;
using Vk.Model;
using Vk.Provider;

namespace Downloader
{
    public class Resolver : IResolver
    {
        private readonly LoggerService<Resolver> _logger = new LoggerService<Resolver>();

        public async Task<List<ParseGroup>> GroupResolve(List<int> groupIds)
        {
            
            DatabasesMethods databasesMethods = new();
            _logger.Info("Загружаю данные из Viqube...");
            Record groupsFromDatabase = databasesMethods.GetRecordsAsync("Timlab_project", "VK_Groups").Result;
            Record groupsToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };
            List<object> groupsToDelete = new();
            VkProvider vkProvider = new();
            var vkGroups = await vkProvider.GetGroupsInfo(groupIds);
            Record groupsFromVk = ParserVkToViqube.TryParseToViQube(vkGroups);

            if (groupsFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Groups", groupsFromVk);
            }
            else
            {
                foreach (var rowVk in groupsFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in groupsFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0))
                        {
                            isInDB = true;

                            if ((int)rowVk.ElementAt(3) != (int)(long)rowViqube.ElementAt(3))
                            {
                                groupsToDelete.Add(rowViqube.ElementAt(0));
                                groupsToAdd.Values.Add(rowVk);
                            }
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        groupsToAdd.Values.Add(rowVk);
                    }
                }
                if (groupsToDelete.Count != 0)
                    await databasesMethods.DeleteRecordAsync("Timlab_project", "VK_Groups", groupsToDelete);
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Groups", groupsToAdd);
            }
            return vkGroups;
        }


        public async Task UserResolve(int groupId, int count, int offset)
        {
            DatabasesMethods databasesMethods = new();
            _logger.Info("Загружаю данные из Viqube...");
            Record usersFromDatabase = databasesMethods.GetRecordsAsync("Timlab_project", "VK_Users").Result;
            Record usersInGroupsFromDatabase = databasesMethods.GetRecordsAsync("Timlab_project", "VK_Users_in_Groups").Result;
            Record usersToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };
            Record usersInGroupsToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };

            VkProvider vkProvider = new();
            var result = vkProvider.GetGroupMembers(groupId, count, offset).Result;
            var users = result.Item1;
            var usersInGroups = result.Item2;
            Record usersFromVk = ParserVkToViqube.TryParseToViQube(users);
            Record usersInGroupsFromVk = ParserVkToViqube.TryParseToViQube(usersInGroups);

            if (usersFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Users", usersFromVk);
            }
            else
            {
                foreach (var rowVk in usersFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in usersFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0))
                        {
                            isInDB = true;
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        usersToAdd.Values.Add(rowVk);
                    }
                }
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Users", usersToAdd);
            }

            if (usersInGroupsFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Users_in_Groups", usersInGroupsFromVk);
            }
            else
            {
                foreach (var rowVk in usersInGroupsFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in usersInGroupsFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0) &&
                            (int)rowVk.ElementAt(1) == (int)(long)rowViqube.ElementAt(1))
                        {
                            isInDB = true;
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        usersInGroupsToAdd.Values.Add(rowVk);
                    }
                }
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Users_in_Groups", usersInGroupsToAdd);
            }
        }

        public async Task<List<ParseWallPost>> WallPostResolve(int groupId, int count)
        {
            DatabasesMethods databasesMethods = new();
            _logger.Info("Загружаю данные из Viqube...");
            Record wallPostsFromDatabase = await databasesMethods.GetRecordsAsync("Timlab_project", "VK_Posts");
            Record wallPostsToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };
            List<object> wallPostsToDelete = new();
            VkProvider vkProvider = new();
            var vkPosts = vkProvider.WallGet(groupId, count).Result;
            Record wallPostsFromVk = ParserVkToViqube.TryParseToViQube(vkPosts);

            if (wallPostsFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Posts", wallPostsFromVk);
            }
            else
            {
                foreach (var rowVk in wallPostsFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in wallPostsFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0) &&
                            (int)rowVk.ElementAt(1) == (int)(long)rowViqube.ElementAt(1))
                        {
                            isInDB = true;

                            if ((int)rowVk.ElementAt(2) != (int)(long)rowViqube.ElementAt(2) ||
                                (int)rowVk.ElementAt(3) != (int)(long)rowViqube.ElementAt(3) ||
                                (int)rowVk.ElementAt(4) != (int)(long)rowViqube.ElementAt(4))
                            {
                                wallPostsToDelete.Add(rowViqube.ElementAt(6));
                                wallPostsToAdd.Values.Add(rowVk);
                            }
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        wallPostsToAdd.Values.Add(rowVk);
                    }
                }
                if (wallPostsToDelete.Count != 0) 
                    await databasesMethods.DeleteRecordAsync("Timlab_project", "VK_Posts", wallPostsToDelete);
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Posts", wallPostsToAdd);
            }

            return vkPosts;
        }

        public async Task CommentResolve(int groupId, int postId, int count)
        {
            DatabasesMethods databasesMethods = new();
            _logger.Info("Загружаю данные из Viqube...");
            Record commentsFromDatabase = await databasesMethods.GetRecordsAsync("Timlab_project", "VK_Comments");
            Record commentsToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };
            List<object> commentsToDelete = new();
            VkProvider vkProvider = new();
            var vkComments = vkProvider.GetComments(groupId, postId, count).Result;
            Record commentsFromVk = ParserVkToViqube.TryParseToViQube(vkComments);

            if (commentsFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Comments", commentsFromVk);
            }
            else
            {
                foreach (var rowVk in commentsFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in commentsFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0) &&
                            (int)rowVk.ElementAt(2) == (int)(long)rowViqube.ElementAt(2) &&
                            (int)rowVk.ElementAt(3) == (int)(long)rowViqube.ElementAt(3))
                        {
                            isInDB = true;

                            if ((int)rowVk.ElementAt(4) != (int)(long)rowViqube.ElementAt(4))
                            {
                                commentsToDelete.Add(rowViqube.ElementAt(6));
                                commentsToAdd.Values.Add(rowVk);
                            }
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        commentsToAdd.Values.Add(rowVk);
                    }
                }

                if (commentsToDelete.Count != 0)
                    await databasesMethods.DeleteRecordAsync("Timlab_project", "VK_Comments", commentsToDelete);
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Comments", commentsToAdd);
            }   
        }

        public async Task LikeResolve(int groupId, int postId, int count)
        {
            DatabasesMethods databasesMethods = new();
            _logger.Info("Загружаю данные из Viqube...");
            Record likesFromDatabase = await databasesMethods.GetRecordsAsync("Timlab_project", "VK_Likes");
            Record likesToAdd = new Record()
            {
                Columns = new List<string>(),
                Values = new List<List<object>>()
            };
            VkProvider vkProvider = new();
            var vkLikes = vkProvider.GetWallPostLikes(groupId, postId, count).Result;
            Record likesFromVk = ParserVkToViqube.TryParseToViQube(vkLikes);

            if (likesFromDatabase.Values == null)
            {
                _logger.Warn("Данных в Viqube нет!");
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Likes", likesFromVk);
            }
            else
            {
                foreach (var rowVk in likesFromVk.Values)
                {
                    bool isInDB = false;
                    foreach (var rowViqube in likesFromDatabase.Values)
                    {
                        if ((int)rowVk.ElementAt(0) == (int)(long)rowViqube.ElementAt(0) &&
                            (int)rowVk.ElementAt(1) == (int)(long)rowViqube.ElementAt(1))
                        {
                            isInDB = true;
                            break;
                        }
                    }

                    if (!isInDB)
                    {
                        likesToAdd.Values.Add(rowVk);
                    }
                }
                await databasesMethods.PostRecordsAsync("Timlab_project", "VK_Likes", likesToAdd);
            }
        }
    }
}
