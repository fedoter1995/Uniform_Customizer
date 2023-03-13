using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AnimatedObject))]
public class CustomizationHandler : MonoBehaviour, ISavable
{
    [SerializeField]
    private List<CustomObject> _objectsPrefabs = new List<CustomObject>();

    [SerializeField]
    protected Button _nextButton;
    [SerializeField]
    protected Button _previousButton;


    protected List<CustomObject> objects;

    protected CustomObject activeObject;

    protected BookmarkSwitch _switch;


    public CustomObject ActiveObject => activeObject;


    public virtual void Initialize(BookmarkSwitch Switch)
    {
        _switch = Switch;

        _nextButton.onClick.AddListener(NextObject);
        _previousButton.onClick.AddListener(PreviousObject);

        _switch.Initialize(activeObject.Colors);
        _switch.OnChangeColorEvent += ChangeLayerColor;
    }

    public virtual void OnChangePage()
    {
        _nextButton.onClick.RemoveAllListeners();
        _previousButton.onClick.RemoveAllListeners();
        _switch.OnChangeColorEvent -= ChangeLayerColor;

    }


    public void SetObjectData(Dictionary<string, object> data)
    {
        objects = new List<CustomObject>();

        if (data != null)
        {
            JArray array = (JArray)data["Objects"];
            var objects = array.ToObject<List<Dictionary<string, object>>>();
            var activeObjId = data["Active_Object"].ToString();

            foreach (Dictionary<string, object> objData in objects)
            {
                JArray hexArray = (JArray)objData["Colors"];
                var hexColors = hexArray.ToObject<List<string>>();

                var colors = new List<Color>();
                foreach(string hex in hexColors)
                {
                    Color newColor = new Color();
                    
                    
                    if (ColorUtility.TryParseHtmlString(hex, out newColor))
                    {
                        colors.Add(newColor);
                    }
                }

                var obj = CreateObject((string)objData["Id"], colors);

                if (obj.Id == activeObjId)
                    activeObject = obj;
            }

        }
        else
        {
            foreach (CustomObject obj in _objectsPrefabs)
            {
                CreateObject(obj);
                activeObject = objects[0];
            }
        }
        activeObject.Enter();
    }
    public Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>();
        var dataList = new List<Dictionary<string, object>>();
        for(int i = 0; i < objects.Count; i++)
        {
            dataList.Add(objects[i].GetObjectData());
        }
        data.Add("Objects", dataList);
        data.Add("Active_Object", activeObject.Id);

        return data;
    }

    private CustomObject CreateObject(string id, List<Color> colors)
    {
        var prefab = GameManager.GetObject(id);
        var newobject = Instantiate(prefab, transform);
        newobject.Initialize(colors);

        objects.Add(newobject);

        newobject.Exit();

        return newobject;
    }
    private CustomObject CreateObject(CustomObject prefab)
    {
        var newObject = Instantiate(prefab, transform);

        newObject.Initialize();

        objects.Add(newObject);

        newObject.Exit();

        return newObject;
    }
    private void NextObject()
    {
        activeObject.Exit();

        int i = objects.FindIndex(uniform => uniform == activeObject);

        if (i == objects.Count - 1)
            activeObject = objects[0];
        else
            activeObject = objects[i + 1];

        activeObject.Enter();

        _switch.ChangeColors(activeObject.Colors);
    }
    private void PreviousObject()
    {
        activeObject.Exit();

        int i = objects.FindIndex(uniform => uniform == activeObject);

        if (i == 0)
            activeObject = objects[objects.Count - 1];
        else
            activeObject = objects[i - 1];

        activeObject.Enter();
        _switch.ChangeColors(activeObject.Colors);
    }
    private void ChangeLayerColor(ColorBookmark bookmark)
    {
        activeObject.ChangeLayerColor(bookmark);
    }

}
