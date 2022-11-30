namespace Services.FileService
{
	public static class FileService
	{
		private static readonly JsonSerializationService _jsonSerializationService;

		static FileService()
		{
			_jsonSerializationService = new JsonSerializationService();
		}
	}
}
