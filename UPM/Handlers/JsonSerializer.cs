using System.Text;
using UnityEngine;

namespace SleeplessDev
{
	public class JsonSerializer : ISerializer
	{
		public byte[] Serialize<T>(T obj)
		{
			string json = JsonUtility.ToJson(obj);
			return Encoding.UTF8.GetBytes(json); 
		}

		public T Deserialize<T>(string json)
		{
			return JsonUtility.FromJson<T>(json);
		}

		/*//You can also use using Newtonsoft.Json; library and it 
		public byte[] Serialize<T>(T obj)
		{
			// Using JsonUtility for Unity objects
			if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
			{
				string json = JsonUtility.ToJson(obj);
				return System.Text.Encoding.UTF8.GetBytes(json);
			}
			// Using Json.NET for other objects
			else
			{
				string json = JsonConvert.SerializeObject(obj);
				return System.Text.Encoding.UTF8.GetBytes(json);
			}
		}

		public T Deserialize<T>(string json)
		{
			// Using JsonUtility for Unity objects
			if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
			{
				return JsonUtility.FromJson<T>(json);
			}
			// Using Json.NET for other objects
			else
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
		}*/



	}
}
