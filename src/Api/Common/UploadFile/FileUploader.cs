using Api.Common.Configurations;

namespace Api.Common;

public class FileUploader
{
    private readonly string[] _permittedExtensions = { ".TXT", ".PNG", ".JPG", ".WEBP" };
    private readonly UploadFileOptions _options;
    public FileUploader(IConfiguration configuration)
    {
        _options = new();
        configuration.GetSection(UploadFileOptions.UploadFile).Bind(_options);
        ArgumentNullException.ThrowIfNull(_options);
    }

    public async Task UploadFile(IList<IFormFile> files)
    {
        if (files is null)
        {
            throw new FileUploaderException(FileUploaderMessages.SelectFile);
        }

        if (files.Count == 0)
        {
            throw new FileUploaderException(FileUploaderMessages.SelectFile);
        }

        foreach (var file in files)
        {
            await this.UploadFile(file);
        }
    }
    public async Task<string> UploadFile(IFormFile file)
    {
        if (file is null || file.Length <= 0)
        {
            throw new FileUploaderException(FileUploaderMessages.SelectFile);
        }

        if (_options.StoredImagesFolder is null || _options.StoredImagesPath is null)
        {
            throw new FileUploaderException(FileUploaderMessages.EnterStoredFilesPath);
        }

        ValidateFileExtension(file, _permittedExtensions);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var storedFilesPath = _options.StoredImagesPath;
        var absolutePath = Path.Combine(storedFilesPath, fileName);
        var relativePath = $"/{_options.StoredImagesFolder}/{fileName}";

        using (var stream = File.Create(absolutePath))
        {
            await file.CopyToAsync(stream);
        }

        return relativePath;
    }

    private static void ValidateFileExtension(IFormFile file, string[] permittedExtensions)
    {
        var ext = Path.GetExtension(file.FileName).ToUpperInvariant();

        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
        {
            throw new FileUploaderException(FileUploaderMessages.FileExtensionNotAllowed);
        }
    }
}