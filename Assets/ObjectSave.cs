using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Linq;

public class ObjectSave : MonoBehaviour
{
    string userID;
    DatabaseReference databaseReference;
    public GameObject defaultPrefab;

    void Start()
    {
        userID = UserManager.instance.getUserID();
        Debug.Log(userID);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveObjectData()
    {
        List<ObjectData> objectDataList = new List<ObjectData>();

        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            ObjectData data = new ObjectData
            {
                id = System.Guid.NewGuid().ToString(),
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                scale = obj.transform.localScale,
            };
            objectDataList.Add(data);
        }

        ObjectDataArray dataArray = new ObjectDataArray
        {
            objects = objectDataList.ToArray()
        };

        ObjectDataEntry entry = new ObjectDataEntry
        {
            userId = userID,
            dataArray = dataArray
        };

        string json = JsonUtility.ToJson(entry);

        databaseReference.Child("objectData").Push().SetRawJsonValueAsync(json);
    }

    async void LoadObjectData()
    {
        DatabaseReference objectDataRef = databaseReference.Child("objectData");
        DataSnapshot snapshot = await objectDataRef.GetValueAsync();

        if (snapshot == null || !snapshot.Exists)
        {
            Debug.Log("No object data found");
            return;
        }

        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            string jsonData = childSnapshot.GetRawJsonValue();
            ObjectDataEntry entry = JsonUtility.FromJson<ObjectDataEntry>(jsonData);

            string loadedUserId = entry.userId;
            ObjectData[] loadedObjectDataArray = entry.dataArray.objects;

            Debug.Log("Loaded data for user: " + loadedUserId);

            SpawnObjectsFromData(loadedObjectDataArray);
        }
    }

    void SpawnObjectsFromData(ObjectData[] objectDataArray)
    {
        foreach (ObjectData objData in objectDataArray)
        {
            GameObject obj = Instantiate(defaultPrefab, objData.position, objData.rotation, transform);
            Debug.Log("instatiated!");

            obj.transform.localScale = objData.scale;
        }
    }

}
