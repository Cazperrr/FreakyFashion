using FreakyFashion.Models;

namespace FreakyFashion.DTOs;

public record CategoriesDTO(
    int Id,
    string Name,
    string Image,
    string UrlSlug,
    List<Products>? Products
    );