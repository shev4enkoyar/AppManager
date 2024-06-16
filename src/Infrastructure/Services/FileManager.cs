using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Abstractions;

namespace AppManager.Infrastructure.Services;

public class FileManager : IFileManager
{
    private string FileDirectoryPath { get; } = Path.Combine(Environment.CurrentDirectory, "uploaded-files");
    public async Task<string?> UploadFileAsync(IFile file, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(FileDirectoryPath))
        {
            Directory.CreateDirectory(FileDirectoryPath);
        }

        try
        {
            var filePath = Path.Combine(FileDirectoryPath, $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
            await using FileStream fileStream = File.Create(filePath);
            await file.CopyToAsync(fileStream, cancellationToken);

            return filePath;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<FileStream> DownloadFileAsync(string path, CancellationToken cancellationToken = default)
    {
        return File.OpenRead(path);
    }

    public async Task DeleteFileAsync(string path, CancellationToken cancellationToken = default)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
