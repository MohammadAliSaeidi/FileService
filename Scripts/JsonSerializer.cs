using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Chromium.Utilities.Singleton;

namespace Services.FileService
{
	public class JsonSerializer : IDisposable
	{
		public static JsonSerializer Instance => SingletonFactory<JsonSerializer>.Instance;

		public async Task<SavingResult> SaveAsJsonAsync<T>(T objectToSave, string savePath)
		{
			SavingResult result = null;
			await Task.Run(() =>
			{
				result = SaveAsJson<T>(objectToSave, savePath);
			});
			return result;
		}

		public SavingResult SaveAsJson<T>(T objectToSave, string savePath)
		{
			try
			{
				Debug.Log("↓ Saving file..." +
							$"\n<b>Save path:</b> {savePath}");

				var jsonString = JsonUtility.ToJson(objectToSave);
				File.WriteAllText(savePath, jsonString);

				Debug.Log("<color=#00B149><b>File saved successfully</b></color>" +
							$"<b>\nSave path:</b> {savePath}");

				return new SavingResult(null, true);
			}
			catch (Exception e)
			{
				Debug.LogError("<b><color=#CF6679>Saving file was failed.</color></b>" +
								$"\n<b>Error message:</b> {e.Message}");

				return new SavingResult(e, false);
			}
		}

		public async Task<T> LoadFromJsonAsync<T>(string filePath)
		{
			T result = default;
			await Task.Run(() =>
			{
				result = LoadFromJson<T>(filePath);
			});
			return result;
		}

		public T LoadFromJson<T>(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					var jsonString = File.ReadAllText(filePath.ToString());
					var deserializedObject = JsonUtility.FromJson<T>(jsonString);

					Debug.Log("<color=#00B149><b>File loaded successfully</b></color>" +
					$"<b>\nfile path:</b> {filePath}");

					return deserializedObject;
				}
				else
				{
					Debug.Log("The file " + $"<color=red><b>does not exists</b></color>\n<b>File path:</b> {filePath}");
				}
			}
			catch (Exception e)
			{
				Debug.LogError("<b><color=#CF6679>Loading file was failed.</color></b>" +
								$"\n<b>Error message:</b> {e.Message}");
			}

			return default;
		}

		public void Dispose()
		{
			Instance.Dispose();
		}
	}
}
