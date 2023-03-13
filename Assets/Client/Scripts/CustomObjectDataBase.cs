using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CustomObject/DataBase")]
public class CustomObjectDataBase : ScriptableObject
{
    [SerializeField]
    private string _name = "Default_Data_Base";
    [SerializeField]
    private List<CustomObject> _objects = new List<CustomObject>();

    public string Name => _name;


    public CustomObject GetObject(string id)
    {
        var objects = _objects.FindAll(obj => obj.Id == id);

        CustomObject obj = null;

        switch (objects.Count)
        {
            case 1:
                {
                    obj = objects[0];
                    break;
                }
            case > 1:
                {
                    throw new System.Exception($"Found more than one item with id = {id}");
                }
            case 0:
                {
                    throw new System.Exception($"No item with id = {id} found");
                }
        }

        return obj;
    }

}
