using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationRegulator : MonoBehaviour
{
    public int maxInstances = 200;

    void Update()
    {
        int childCount = transform.childCount;
        if (childCount > maxInstances)
        {
            // Keep one instance and destroy the rest
            for (int i = 0; i < childCount - 1; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
