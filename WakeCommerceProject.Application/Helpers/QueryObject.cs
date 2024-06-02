namespace WakeCommerceProject.Application.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;

        public string? SKU { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;
    }
}