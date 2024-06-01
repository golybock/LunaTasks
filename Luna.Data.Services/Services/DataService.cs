using Luna.Data.Repositories.Repositories;
using Luna.Models.Data.Database.Data;
using Luna.Tools.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Luna.Data.Services.Services;

public class DataService : IDataService
{
	private readonly IDataRepository _dataRepository;

	private const string BaseDirectory = "Files";

	private string Url { get; set; }

	public DataService(IDataRepository dataRepository, IConfiguration configuration)
	{
		_dataRepository = dataRepository;

		Url = configuration["Url"] ?? throw new Exception("Url is null");
	}

	public async Task<byte[]?> GetFileAsync(Guid id)
	{
		var file = await _dataRepository.GetFileAsync(id);

		if (file == null)
		{
			return null;
		}

		return await File.ReadAllBytesAsync(Path.Combine(BaseDirectory, file.Path));
	}

	public async Task<IEnumerable<string>> GetFilesAsync(Guid userId, bool deleted = false)
	{
		var files = await _dataRepository.GetFilesAsync(deleted);

		var paths = new List<string>(files.Count());

		foreach (var fileDatabase in files)
		{
			var format = Path.GetExtension(fileDatabase.Path);
			paths.Add(Path.Combine(Url, fileDatabase.Path.Replace(format, "")));
		}

		return paths;
	}

	public async Task<IEnumerable<string>> GetFilesAsync(Guid workspaceId, Guid userId, bool deleted = false)
	{
		var files = await _dataRepository.GetFilesAsync(workspaceId, deleted);

		var paths = new List<string>(files.Count());

		foreach (var fileDatabase in files)
		{
			var format = Path.GetExtension(fileDatabase.Path);
			paths.Add(Path.Combine(Url, fileDatabase.Path.Replace(format, "")));
		}

		return paths;
	}

	public async Task<IActionResult> CreateFileAsyncAsync(IFormFile formFile, Guid workspaceId, Guid userId)
	{
		var format = Path.GetExtension(formFile.FileName);
		var fileType = formFile.ContentType;

		var fileId = Guid.NewGuid();

		FileDatabase fileDatabase;

		if (fileType.Contains("image"))
		{
			fileDatabase = new FileDatabase()
			{
				Id = fileId,
				Path = fileId + format,
				FileType = FileType.Image,
				UploadedByUserId = userId,
				WorkspaceId = workspaceId
			};
		}
		else
		{
			fileDatabase = new FileDatabase()
			{
				Id = fileId,
				Path = fileId + format,
				FileType = FileType.Attachment,
				UploadedByUserId = userId,
				WorkspaceId = workspaceId
			};
		}

		try
		{
			await _dataRepository.CreateFileAsyncAsync(fileDatabase);

			if (!Directory.Exists(BaseDirectory))
			{
				Directory.CreateDirectory(BaseDirectory);
			}

			await using StreamWriter sw = new StreamWriter(Path.Combine(BaseDirectory, fileDatabase.Path));

			await formFile.CopyToAsync(sw.BaseStream);

			return new OkObjectResult(fileDatabase.Id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return new BadRequestResult();
		}
	}


	public async Task<bool> DeleteFileAsync(Guid id)
	{
		var file = await _dataRepository.GetFileAsync(id);

		if (file == null)
			return false;

		File.Delete(Path.Combine(BaseDirectory, file.Path));

		return await _dataRepository.DeleteFileAsync(id);
	}
}