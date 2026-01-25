using KayraExport.API.Dto;

namespace KayraExport.API.Services;

public interface IProductService
{
    Task<ProductListDto> GetListAsync(string? searchTerm, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CreateProductDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
