using Api.Common.Configurations;
using FileSignatures;
using FileSignatures.Formats;

namespace Api.Common;

public class FileUploader
{
    private readonly string[] _permittedExtensions = { ".PNG", ".JPG", ".JPEG", ".WEBP" };
    private readonly UploadFileOptions _options;
    private readonly IFileFormatInspector _inspector;
    public FileUploader(IConfiguration configuration,
     IFileFormatInspector inspector)
    {
        _options = new();
        configuration.GetSection(UploadFileOptions.UploadFile).Bind(_options);
        _inspector = inspector;
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

        if (file.Length > _options.FileSizeLimit)
        {
            throw new FileUploaderException(FileUploaderMessages.EnterStoredFilesPath);
        }

        ValidateFileExtension(file, _permittedExtensions);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var storedFilesPath = _options.StoredImagesPath;
        var absolutePath = Path.Combine(storedFilesPath, fileName);
        var relativePath = $"/{_options.StoredImagesFolder}/{fileName}";

        using(var stream = file.OpenReadStream())
        {
            this.ValidateSignature(stream);
        }

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

    private void ValidateSignature(Stream stream)
    {
        var format = _inspector.DetermineFileFormat(stream);

        if (format is Pdf)
        {
            return;
        }

        if (format is Png)
        {
            return;
        }

        if (format is Image)
        {
            return;
        }

        if (format is Webp)
        {
            return;
        }

        if (format is Jpeg)
        {
            return;
        }
        throw new FileUploaderException(FileUploaderMessages.FileNotAllowed);
    }
}