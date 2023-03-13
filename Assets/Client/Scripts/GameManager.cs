using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField]
    private SaveDataContainer _dataContainer;
    [SerializeField]
    private CustomObjectDataBase _dataBase;


    [SerializeField]
    private string _saveName = "Save_2";

    private SaveData save;

    private List<ISavable> savableObjects;

    private static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = new GameObject("[GAME MANAGER]");
                m_instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return m_instance;
        }

    }

    void Awake()
    {
        if (m_instance == null)
            m_instance = this;

        instance.save = GetSaveData();

        DontDestroyOnLoad(gameObject);
    }

    private static GameManager m_instance;

    private static SaveData GetSaveData()
    {
        var data = JsonSerealization<SaveData>.Deserialize("Save");

        if (data is null)
            data = new SaveData();

        return data;
    }
    public static Dictionary<string,object> LoadObjectData(string objName)
    {
        return instance.save.GetObjectData(objName);
    }
    public static void SaveObjectData(Dictionary<string, Dictionary<string, object>> data)
    {
        instance.save.SetObjectData(data);
        JsonSerealization<SaveData>.Serialize(instance.save, "Save");
    }
    public static CustomObject GetObject(string id)
    {
        return instance._dataBase.GetObject(id);
    }

    public static void CloseProgramm()
    {
        Application.Quit();
    }

}
