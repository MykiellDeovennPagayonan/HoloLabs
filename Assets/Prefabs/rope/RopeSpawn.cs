using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab, parentObject;

    [SerializeField]
    [Range(1, 1000)]
    int length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("ropeSegment"))
            {
                Destroy(temp);
            }

            reset = false;
        }

        if (spawn)
        {
            Spawn();

            spawn = false;
        }
    }

    public void Spawn()
    {
        int count = (int)(length / partDistance);

        for (int x = 0; x < count; x++)
        {
            GameObject temp;

            temp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance * (x + 1), transform.position.z), Quaternion.identity, parentObject.transform);
            temp.transform.eulerAngles = new Vector3(180, 0, 0);

            temp.name = parentObject.transform.childCount.ToString();

            if (x == 0)
            {
                Destroy(temp.GetComponent<CharacterJoint>());
            }
            else
            {
                temp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
