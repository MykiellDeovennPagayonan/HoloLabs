using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string id;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public PrimitiveType shapeType;
    public Color shapeColor;

    public ObjectData()
    {
        id = "";
        position = Vector3.zero;
        rotation = Quaternion.identity;
        scale = Vector3.one;
        shapeType = PrimitiveType.Cube;
        shapeColor = Color.gray;
    }

    public ObjectData(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, PrimitiveType _shapeType, Color _shapeColor)
    {
        id = _id;
        position = _position;
        rotation = _rotation;
        scale = _scale;
        shapeType = _shapeType;
        shapeColor = _shapeColor;
    }
}

[System.Serializable]
public class ObjectDataArray
{
    public ObjectData[] objects;
}

[System.Serializable]
public class ObjectDataEntry
{
    public string userId;
    public ObjectDataArray dataArray;
}
