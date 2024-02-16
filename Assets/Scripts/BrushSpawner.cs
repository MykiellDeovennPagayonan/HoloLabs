using UnityEngine;
using UnityEngine.UI;

public class BrushSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform cursor;
    public float spawnDistance = 1f;
    private GameObject spawnedObject;
    public GameObject ObjectPool;
    public float scaleSensitivity = 0.2f;
    public Text gravityMessage;
    bool gravityOn = false;

    public RectTransform scalePanel;

    private Vector2 touchStartPos;
    private void Start()
    {
        Rigidbody rb = objectPrefab.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = objectPrefab.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.isKinematic = true;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 spawnPosition = Camera.main.transform.position + cameraForward * spawnDistance;
        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        spawnedObject.transform.parent = cursor;
        spawnedObject.layer = 6;

        gravityMessage.text = "gravity is disabled";
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
                    if (RectTransformUtility.RectangleContainsScreenPoint(scalePanel, touchStartPos))
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
            spawnedObject.transform.rotation = Camera.main.transform.rotation;
            spawnedObject.transform.position = spawnPosition;
        }

        if (cursor.transform.childCount == 0)
        {
            SpawnObject();
        }
    }
    void ScaleObject(float deltaPositionY)
    {
        float scaleFactor = 1f + deltaPositionY * Time.deltaTime * scaleSensitivity;

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
            NewSpawnedObject.transform.rotation = Camera.main.transform.rotation;
            NewSpawnedObject.layer = 6;
            NewSpawnedObject.transform.localScale = spawnedObject.transform.localScale;
            Rigidbody rb = NewSpawnedObject.GetComponent<Rigidbody>();

            if (gravityOn)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            } else
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogError("Prefab is not assigned!");
        }
    }

    public void ToggleGrvity()
    {
        if (gravityOn)
        {
            DisableGravity();
        } else
        {
            EnableGravity();
        }
    }

    void EnableGravity()
    {
        gravityOn = true;
        gravityMessage.text = "gravity is enabled";
    }

    void DisableGravity()
    {
        gravityOn = false;
        gravityMessage.text = "gravity is disabled";
    }
}
