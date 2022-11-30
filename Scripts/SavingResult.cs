using System;

namespace Services.FileService
{
	public class SavingResult
	{
		public readonly Exception exception;
		public readonly bool Successful;

		internal SavingResult(Exception exception, bool successful)
		{
			this.exception = exception;
			Successful = successful;
		}
	}
}
