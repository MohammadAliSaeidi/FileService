using Services.FileService;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FileServiceExample : MonoBehaviour
{
	[Header("non-Async")]
	[SerializeField]
	private Button btn_Save;

	[SerializeField]
	private Button btn_Load;

	[Header("Async")]
	[SerializeField]
	private Button btn_SaveAsync;

	[SerializeField]
	private Button btn_LoadAsync;

	private void Start()
	{
		btn_Save.onClick.AddListener(() => SaveExampleObjectToJson());
		btn_Load.onClick.AddListener(() => LoadExampleObjectFromJson());

		btn_SaveAsync.onClick.AddListener(() => SaveExampleObjectToJsonAsync());
		btn_LoadAsync.onClick.AddListener(() => LoadExampleObjectFromJsonAsync());
	}


	public void SaveExampleObjectToJson()
	{
		var objectToSave = new FileServiceExampleClass(
			24.7f,
			"This an example text",
			new System.Collections.Generic.List<Vector3> { new Vector3(12.5f, 1.65f, 5.62f) });
		JsonSerializer.Instance.SaveAsJson(objectToSave, "Assets/ExampleObjectToJsonAsync.json");
	}

	public void SaveExampleObjectToJsonAsync()
	{
		StartCoroutine(Co_SaveExampleObjectToJsonAsync());
	}

	public IEnumerator Co_SaveExampleObjectToJsonAsync()
	{
		var objectToSave = new FileServiceExampleClass(
			24.7f,
			"This an example text",
			new System.Collections.Generic.List<Vector3> { new Vector3(12.5f, 1.65f, 5.62f) });

		var savingTask = JsonSerializer.Instance.SaveAsJsonAsync(objectToSave, "Assets/ExampleObjectToJsonAsync.json");
		yield return new WaitUntil(() => savingTask.IsCompleted);
	}


	public void LoadExampleObjectFromJson()
	{
		var loadedObject = JsonSerializer.Instance.LoadFromJson<FileServiceExampleClass>("Assets/ExampleObjectToJsonAsync.json");
		if (loadedObject != null)
			Debug.Log($"aFloat: {loadedObject.aFloat}\naString: {loadedObject.aString}\naVector3List[0]: {loadedObject.aVector3List[0]}");
	}

	public void LoadExampleObjectFromJsonAsync()
	{
		StartCoroutine(Co_LoadExampleObjectFromJsonAsync());
	}

	public IEnumerator Co_LoadExampleObjectFromJsonAsync()
	{
		var loadingTask = JsonSerializer.Instance.LoadFromJsonAsync<FileServiceExampleClass>("Assets/ExampleObjectToJsonAsync.json");
		yield return new WaitUntil(() => loadingTask.IsCompleted);

		var loadedObject = loadingTask.Result;
		if (loadedObject != null)
			Debug.Log($"aFloat: {loadedObject.aFloat}\naString: {loadedObject.aString}\naVector3List[0]: {loadedObject.aVector3List[0]}");
	}
}
