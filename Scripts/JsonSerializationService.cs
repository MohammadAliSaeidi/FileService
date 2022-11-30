using System;
using System.IO;
using UnityEngine;

namespace Services.FileService
{
	internal class JsonSerializationService
	{
		public static SavingResult SaveAsJson<T>(T objectToSave, string savePath)
		{
			try
			{
				var jsonString = JsonUtility.ToJson(objectToSave);
				File.WriteAllText(savePath, jsonString);
				return new SavingResult(null, true);
			}
			catch (Exception e)
			{
				Debug.LogError("<color=#CF6679>Saving file was failed." +
					$"\n<color=#CF6679><b>Error message:</b></color> {e.Message}");

				return new SavingResult(e, false);
			}
		}

		public static T DeserializeFromJson<T>(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					var jsonString = File.ReadAllText(filePath.ToString());
					var deserializedObject = JsonUtility.FromJson<T>(jsonString);
					return deserializedObject;
				}
				else
				{
					Debug.Log("The file " + $"<color=red><b>does not exists</b></color>\nFile path: {filePath}");
				}
			}
			catch (Exception e)
			{
				Debug.LogError("<color=#CF6679>Loading file was failed." +
								$"\n<color=#CF6679><b>Error message:</b></color> {e.Message}");
			}

			return default;
		}
	}
}
