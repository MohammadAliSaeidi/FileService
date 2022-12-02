using System.Collections.Generic;
using UnityEngine;

namespace Services.FileService
{
	[System.Serializable]
	public class FileServiceExampleClass
	{
		public float aFloat;
		public string aString;
		public List<Vector3> aVector3List;

		public FileServiceExampleClass(float aFloat, string aString, List<Vector3> aVector3List)
		{
			this.aFloat = aFloat;
			this.aString = aString;
			this.aVector3List = aVector3List;
		}
	}
}