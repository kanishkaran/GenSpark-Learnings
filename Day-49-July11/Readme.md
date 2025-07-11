# Integrating Azure Blob Storage to Backend

## Upload File

### Before (Local Disk Storage)

The file were saved in the local directory.

```csharp
private async Task<string> SaveFileToDisk(IFormFile file, string categoryName)
{
    var folder = Path.Combine(_env.ContentRootPath, $"Uploads/{categoryName}");
    Directory.CreateDirectory(folder);

    var mediaType = await _mediaTypeService.GetMediaTypeByContentType(file.ContentType);
    var filePath = Path.Combine(folder, $"{Guid.NewGuid()}{mediaType.Extension}");

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return filePath;
}
```

### After (Azure Blob Storage)

```csharp
private async Task<string> SaveFileToDisk(IFormFile file, string categoryName)
{
    var mediaType = await _mediaTypeService.GetMediaTypeByContentType(file.ContentType);
    var fileName = $"{Guid.NewGuid()}{mediaType.Extension}";
    var blobPath = $"{categoryName}/{fileName}";

    var blobClient = _blobServiceClient.GetBlobClient(blobPath);

    using (var stream = file.OpenReadStream())
    {
        await blobClient.UploadAsync(stream);
    }
    return blobPath;
}
```

---

## Download File

### Before (Local Disk Storage)

```csharp
var filePath = Path.Combine(_env.ContentRootPath, $"Uploads/{category.CategoryName}", fileVersion.FilePath);

if (!File.Exists(filePath))
    throw new FileNotFoundException("File not found on disk.", filePath);

var bytes = await File.ReadAllBytesAsync(filePath);

var mediaType = await _mediaTypeRepository.GetByIdAsync(fileVersion.ContentTypeId);

return new FileDownloadDto
{
    ContentType = mediaType.TypeName ?? "application/octet-stream",
    FileContent = bytes,
    FileName = $"v{versionNumber}_{fileArchive.FileName}"
};
```

### After (Azure Blob Storage)

```csharp
var blobPath = $"{category.CategoryName}/{fileVersion.FilePath}";
var blobClient = _blobServiceClient.GetBlobClient(blobPath);

if (!await blobClient.ExistsAsync())
{
    throw new FileNotFoundException("No such file");
}

var download = await blobClient.DownloadContentAsync();

var mediaType = await _mediaTypeRepository.GetByIdAsync(fileVersion.ContentTypeId);

return new FileDownloadDto
{
    ContentType = mediaType.TypeName ?? "application/octet-stream",
    FileContent = download.Value.Content.ToArray(),
    FileName = $"v{versionNumber}_{fileArchive.FileName}"
};
```

---

## Logging to Azure Blob Storage

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "WarehouseFileArchiverAPI")
    .WriteTo.Console()
    .WriteTo.AzureBlobStorage(
        connectionString: builder.Configuration.GetConnectionString("AzureBlobStorage"),
        storageContainerName: "logs",
        storageFileName: "log-{yyyyMMdd}.txt"
    )
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```
