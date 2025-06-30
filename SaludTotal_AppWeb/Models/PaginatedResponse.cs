namespace SaludTotal_AppWeb.Models
{
    public class PaginatedResponse<T>
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new();
    }
}


