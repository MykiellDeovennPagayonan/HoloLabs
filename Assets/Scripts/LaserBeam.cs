using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;

    GameObject laserObject;
    LineRenderer laser;
    List<Vector3> laserIndecies = new List<Vector3>();

    Dictionary<string, float> RefractiveMaterials = new Dictionary<string, float>()
    {
        {"air", 1.0f },
        {"glass", 1.5f }
    };
    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir; 

        this.laser = this.laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndecies.Add(pos); 

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndecies.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndecies.Count;

        foreach (Vector3 index in laserIndecies)
        {
            laser.SetPosition(count, index);

            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;

            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);   
        }
        else if (hitInfo.collider.gameObject.tag == "Refract")
        {
            Vector3 pos = hitInfo.point;

            laserIndecies.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.001f + pos.x, Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.001f + pos.y, Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.001f + pos.z);

            float n1 = RefractiveMaterials["air"];
            float n2 = RefractiveMaterials["glass"];

            Vector3 norm = hitInfo.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);

            Ray ray1 = new Ray(newPos1, refractedVector);

            Vector3 newRayStartPos = ray1.GetPoint(1.5f);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);

            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 1.6f, 1))
            {
                laserIndecies.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point, refractedVector2, laser); 

            //CastRay(newPos1, refractedVector, laser);
        }
        else
        {
            laserIndecies.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        incident.Normalize();

        Vector3 refractedVector = (n1/n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1/n2 * n1/n2), Vector3.Cross(norm, incident)))).normalized;
        return refractedVector;
    }
}
