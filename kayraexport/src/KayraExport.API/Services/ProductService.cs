using KayraExport.API.Dto;
using KayraExport.API.Entities;
using KayraExport.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KayraExport.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductListDto> GetListAsync(string? searchTerm, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _productRepository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(searchTerm) ||
                (p.Description != null && p.Description.Contains(searchTerm)));
        }

        var totalItems = await query.CountAsync(cancellationToken);

        var products = await query
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = products.Select(MapToDto).ToList();

        return new ProductListDto
        {
            Products = productDtos,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return product == null ? null : MapToDto(product);
    }

    public async Task<int> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("ProductName is required", nameof(dto.Name));

        var product = new Product
        {
            Name = dto.Name.Trim(),
            Description = dto.Description ?? string.Empty,
            Price = dto.Price,
            Stock = dto.Stock
        };

        var created = await _productRepository.AddAsync(product, cancellationToken);
        return created.Id;
    }

    public async Task UpdateAsync(int id, CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product not found. Id: {id}");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("ProductName is required", nameof(dto.Name));

        product.Name = dto.Name.Trim();
        product.Description = dto.Description ?? string.Empty;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await _productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product not found. Id: {id}");

        await _productRepository.DeleteAsync(product, cancellationToken);
    }

    private static ProductDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        Stock = p.Stock
    };
}
