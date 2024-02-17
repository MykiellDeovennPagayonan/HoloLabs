using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform centerPoint; // The center point around which the planet orbits
    public float orbitSpeed = 10f; // Speed of the orbit

    private void Update()
    {
        // Orbiting motion calculation using trigonometry
        transform.RotateAround(centerPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
