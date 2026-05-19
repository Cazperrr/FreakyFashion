namespace FreakyFashion.DTOs;

public record CreateProductDTO(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Image,
    string UrlSlug,
    int CategoryId
);