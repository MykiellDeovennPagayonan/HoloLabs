using UnityEngine;

public class Bacertia : MonoBehaviour
{
    public float spawnInterval = 1f; // Interval between spawns in seconds

    void Start()
    {
        InvokeRepeating("SpawnCopy", 1f, spawnInterval); // Invoke the SpawnCopy method repeatedly after 1 second, with the given interval
    }

    void SpawnCopy()
    {
        GameObject newBacterium = Instantiate(gameObject, transform.position, transform.rotation); // Instantiate a copy of itself at the current position and rotation
        newBacterium.transform.parent = transform.parent;
    }
}
