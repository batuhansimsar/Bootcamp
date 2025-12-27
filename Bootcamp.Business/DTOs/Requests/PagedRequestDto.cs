namespace Bootcamp.Business.DTOs.Requests;

/// <summary>
/// Request DTO for paginated list endpoints
/// </summary>
public class PagedRequestDto
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    
    /// <summary>
    /// Page number (1-based). Default: 1
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }
    
    /// <summary>
    /// Number of items per page. Default: 10, Max: 100
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : (value > 100 ? 100 : value);
    }
    
    /// <summary>
    /// Calculate skip count for database query
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
}
