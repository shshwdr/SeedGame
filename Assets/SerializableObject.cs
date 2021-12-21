using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public struct SerializedDictionary<T, U> {
    public List<T> Keys;
    public List<U> Values;
    public Dictionary<T, U> getDictionary()
    {
        Dictionary<T, U> res = new Dictionary<T, U>();
        for (int i = 0; i < Keys.Count; i++)
        {
            res[Keys[i]] = Values[i];
        }
        return res;
    }
    public SerializedDictionary(Dictionary<T, U> dict)
    {
        Keys = dict.Keys.ToList();
        Values = dict.Values.ToList();
    }

    public static implicit operator Dictionary<T, U>(SerializedDictionary<T, U> test)
    {
        return test.getDictionary();
    }
    public static implicit operator SerializedDictionary<T, U>(Dictionary<T, U> test)
    {
        return new SerializedDictionary<T,U>(test);
    }
}


[System.Serializable]
public struct SerializedVector
{
    public float x, y, z;
    public Vector3 GetPos()
    {
        return new Vector3(x, y, z);
    }
    public SerializedVector(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }
    public static implicit operator Vector3(SerializedVector test)
    {
        return test.GetPos();
    }
    public static implicit operator SerializedVector(Vector3 test)
    {
        return new SerializedVector(test);
    }
}


[System.Serializable]
public class CSSerializedObject
{
    public float serializationTime;
    public int version = 0;
    public bool isValid = false;
}
public class SerializableObject : MonoBehaviour
{
    public virtual void Save(SerializedGame save)
    {
    }
    public virtual void Load(SerializedGame save)
    {

    }
}
