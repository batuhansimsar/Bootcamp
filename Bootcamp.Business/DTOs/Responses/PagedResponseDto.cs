namespace Bootcamp.Business.DTOs.Responses;

/// <summary>
/// Response wrapper for paginated data
/// </summary>
/// <typeparam name="T">Type of items in the list</typeparam>
public class PagedResponseDto<T>
{
    public List<T> Items { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public PagedResponseDto()
    {
    }
    
    public PagedResponseDto(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
