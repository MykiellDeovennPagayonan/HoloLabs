using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform cursor;
    public float spawnDistance = 1f;
    private GameObject spawnedObject;
    public GameObject ObjectPool;
    public float rotationSensitivity;
    public float scaleSensitivity;

    public RectTransform verticalRotationPanel;
    public RectTransform horizontalRotationPanel;
    public RectTransform scalePanel; // Reference to the UI panel for scalings

    private Vector2 touchStartPos;
    private void Start()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 spawnPosition = Camera.main.transform.position + cameraForward * spawnDistance;
        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        spawnedObject.transform.parent = cursor;
        spawnedObject.layer = 6;
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (RectTransformUtility.RectangleContainsScreenPoint(verticalRotationPanel, touchStartPos))
                    {
                        RotateVertically(touch.deltaPosition.y);
                    }
                    else if (RectTransformUtility.RectangleContainsScreenPoint(horizontalRotationPanel, touchStartPos))
                    {
                        RotateHorizontally(touch.deltaPosition.x);
                    }
                    else if (RectTransformUtility.RectangleContainsScreenPoint(scalePanel, touchStartPos))
                    {
                        ScaleObject(touch.deltaPosition.y);
                    }
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }

        if (spawnedObject != null)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 spawnPosition = Camera.main.transform.position + cameraForward * spawnDistance;
            spawnedObject.transform.position = spawnPosition;
        }

        if (cursor.transform.childCount == 0)
        {
            SpawnObject();
        }
    }

    void RotateVertically(float deltaPositionY)
    {
        spawnedObject.transform.Rotate(Vector3.right, deltaPositionY * Time.deltaTime * rotationSensitivity, Space.Self);
    }

    void RotateHorizontally(float deltaPositionX)
    {
        spawnedObject.transform.Rotate(Vector3.up, -deltaPositionX * Time.deltaTime * rotationSensitivity, Space.Self);
    }

    void ScaleObject(float deltaPositionY)
    {
        // Calculate the scale factor based on the vertical delta position
        float scaleFactor = 1f + deltaPositionY * Time.deltaTime * scaleSensitivity;

        // Apply the scale factor to the object's local scale
        spawnedObject.transform.localScale *= scaleFactor;
    }

    public void SpawnObject()
    {
        if (objectPrefab != null)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 spawnPosition = Camera.main.transform.position + cameraForward * spawnDistance;
            GameObject NewSpawnedObject = Instantiate(spawnedObject, spawnPosition, Quaternion.identity);
            NewSpawnedObject.transform.parent = ObjectPool.transform;
            NewSpawnedObject.layer = 6;

            NewSpawnedObject.transform.rotation = spawnedObject.transform.rotation;

            // Set the scale of the new object to match the spawned object's scale
            NewSpawnedObject.transform.localScale = spawnedObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Prefab is not assigned!");
        }
    }
}
