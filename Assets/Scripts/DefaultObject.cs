using UnityEngine;

public class DefaultObject : MonoBehaviour
{
    public PrimitiveType shapeType; // Specify the initial shape type in the inspector
    public Color shapeColor = Color.gray;
    void Start()
    {
        ChangeShape(shapeType);
        ChangeColor(shapeColor);
    }

    public void ChangeShape(PrimitiveType newShapeType)
    {
        GameObject primitiveObject = GameObject.CreatePrimitive(newShapeType);
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null && primitiveObject != null)
        {
            meshFilter.mesh = primitiveObject.GetComponent<MeshFilter>().sharedMesh;
            Destroy(primitiveObject);
        }
    }
    public void ChangeColor(Color newColor)
    {
        shapeColor = newColor; // Update the shape color
        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component of the current object

        if (renderer != null)
        {
            renderer.material.color = newColor; // Set the color directly to the material's color property
        }
    }
}
