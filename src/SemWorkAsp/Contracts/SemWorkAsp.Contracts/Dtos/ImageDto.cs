using Microsoft.AspNetCore.Http;

namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Объект бизнес-логики картинки
/// </summary>
public class ImageDto
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Изображение
    /// </summary>
    public IFormFile Image { get; set; }
}