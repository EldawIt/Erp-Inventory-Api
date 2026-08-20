namespace ErpSystem.Contract.Shared
{
    public record PaginatedResult<T>(
    List<T> Items,
    int PageNumber,
    int PageSize,
    int TotalCount
)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
