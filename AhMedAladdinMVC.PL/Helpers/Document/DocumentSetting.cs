namespace AhMedAladdinMVC.PL.Helpers.Document
{
	public static class DocumentSetting
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// 1. Get the full folder path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

			// 2. Ensure the folder exists
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			// 3. Create a unique file name
			string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			// 4. Get the full file path
			string filePath = Path.Combine(folderPath, fileName);

			// 5. Save the file
			using var stream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(stream);

			return fileName;
		}

		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}

}
