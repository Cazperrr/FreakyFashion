namespace FreakyFashion.DTOs;

public record CategoriesDTO(
    int Id,
    string Name,
    string Image,
    string UrlSlug,
    List<ProductsDTO>? Products
    );