namespace FreakyFashion.DTOs;

public record CreateProductDTO(
    string Name,
    string Description,
    decimal Price,
    string Image,
    int CategoryId
);