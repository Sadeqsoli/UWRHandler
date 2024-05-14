public interface ISerializer
{
	byte[] Serialize<T>(T obj);
	T Deserialize<T>(string json);
}