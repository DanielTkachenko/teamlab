namespace Vk.Model.ResponseTypes
{
    public class VkItemsResponse<T>
    {
        public VkItems<T> Response { get; set; }
    }

    public class VkItems<T>
    {
        public int Count { get; set; }
        public List<T> Items { get; set; }
    }
}
