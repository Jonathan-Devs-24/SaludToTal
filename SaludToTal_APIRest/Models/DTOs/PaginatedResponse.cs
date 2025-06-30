namespace SaludToTal_APIRest.Models.DTOs
{
    public class PaginatedResponse<T>
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public List<T> Data { get; set; } = new();
    }
}

