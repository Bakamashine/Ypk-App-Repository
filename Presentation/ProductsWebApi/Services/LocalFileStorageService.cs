using Aplication.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ProductsWebApi.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(
            IWebHostEnvironment environment,
            ILogger<LocalFileStorageService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subFolder)
        {
            if (file is null || file.Length == 0)
                return string.Empty;

            if (await ValidationFile(file))
            {
                // Папка с датой
                var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
                var uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    subFolder,
                    today);

                Directory.CreateDirectory(uploadsFolder);

                // Получаем расширение исходного файла
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                var fileName = $"{Guid.NewGuid():N}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = file.OpenReadStream())
                using (var image = await Image.LoadAsync(stream))
                {
                    if (image.Width > 1200 || image.Height > 1200)
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(1200, 1200),
                            Mode = ResizeMode.Max
                        }));
                    }

                    // 🔥 ВЫБОР КОДИРОВЩИКА ПО РАСШИРЕНИЮ
                    if (fileExtension == ".png")
                    {
                        // Для PNG - сохраняем как PNG
                        var pngEncoder = new PngEncoder();  // нужен using SixLabors.ImageSharp.Formats.Png
                        await image.SaveAsync(filePath, pngEncoder);
                    }
                    else
                    {
                        // Для JPG/JPEG - сжимаем как JPEG
                        var jpegEncoder = new JpegEncoder
                        {
                            Quality = 80
                        };
                        await image.SaveAsync(filePath, jpegEncoder);
                    }
                }

                _logger.LogInformation("Файл сохранен: {FilePath}", filePath);

                return $"/uploads/{subFolder}/{today}/{fileName}";
            }

            throw new Exception("Файл не прошел валидацию");
        }

        public Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return Task.FromResult(false);

                var fullPath = Path.Combine(_environment.WebRootPath,
                    filePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation("Файл удален: {FilePath}", fullPath);

                    // Опционально: удаляем пустую папку с датой
                    CleanUpEmptyDirectory(Path.GetDirectoryName(fullPath));

                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении файла: {FilePath}", filePath);
                return Task.FromResult(false);
            }
        }

        // Вспомогательный метод для удаления пустых папок
        private void CleanUpEmptyDirectory(string? directoryPath)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath))
                    return;

                if (Directory.Exists(directoryPath) &&
                    !Directory.EnumerateFileSystemEntries(directoryPath).Any())
                {
                    Directory.Delete(directoryPath);
                    _logger.LogInformation("Удалена пустая папка: {Directory}", directoryPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Не удалось удалить папку: {Directory}", directoryPath);
            }
        }

        public string GetFileUrl(string relativePath)
        {
            return relativePath;
        }

        private async Task<bool> ValidationFile(IFormFile file)
        {
            // Проверка размера (макс 5 МБ)
            if (file.Length >= 5 * 1024 * 1024)
                return false;

            // Проверка расширения
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return false;

            // Проверка MIME-типа
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
            if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                return false;

            return await Task.FromResult(true);
        }
    }
}