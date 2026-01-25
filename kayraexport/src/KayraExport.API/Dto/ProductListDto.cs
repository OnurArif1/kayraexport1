namespace KayraExport.API.Dto;

public class ProductListDto
{
    public List<ProductDto> Products { get; set; } = new();
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
