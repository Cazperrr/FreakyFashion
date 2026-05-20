using FreakyFashion.Models;

namespace FreakyFashion.DTOs;

public record CreateCategoryDTO(
    string Name,
    string Image
    );