
using System.Collections.Generic;

public interface ISavable
{
    Dictionary<string, object> GetObjectData();
    void SetObjectData(Dictionary<string, object> data);
}

