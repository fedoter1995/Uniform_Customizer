using Newtonsoft.Json;
using UnityEngine;

public class JsonSerealization<T> where T : class
{
    public static void Serialize(T data, string path)
    {
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(path, json);
    }
    public static T Deserialize(string path)
    {
        var json = PlayerPrefs.GetString(path);
        var myObject = JsonConvert.DeserializeObject<T>(json);
        return myObject;
    }
}
