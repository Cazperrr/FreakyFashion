namespace FreakyFashion.DTOs;

public record ProductsDTO(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Image,
    string UrlSlug
);