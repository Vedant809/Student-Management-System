namespace StudentManagementSystem.DTOs
{
    public class PaginationRequestDTO
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
