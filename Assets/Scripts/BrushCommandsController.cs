using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrushCommandsController : MonoBehaviour
{
    public BrushSpawner BrushSpawner;

    private Dictionary<string, Color> colorMap = new Dictionary<string, Color>
    {
        { "red", Color.red },
        { "blue", Color.blue },
        { "black", Color.black },
        { "green", Color.green },
        { "gray", Color.gray },
        { "yellow", Color.yellow },
        { "white", Color.white },
        { "cyan", Color.cyan },
        { "magenta", Color.magenta },
        { "orange", new Color(1.0f, 0.5f, 0.0f) },
        { "purple", new Color(0.5f, 0.0f, 0.5f) },
        { "brown", new Color(0.6f, 0.3f, 0.0f) },
        { "pink", new Color(1.0f, 0.5f, 0.5f) },
        { "skyblue", new Color(0.53f, 0.81f, 0.98f) },
        { "lime", new Color(0.0f, 1.0f, 0.0f) },
        { "gold", new Color(1.0f, 0.84f, 0.0f) },
        { "turquoise", new Color(0.25f, 0.88f, 0.82f) },
        { "maroon", new Color(0.5f, 0.0f, 0.0f) },
        { "navy", new Color(0.0f, 0.0f, 0.5f) },
        { "olive", new Color(0.5f, 0.5f, 0.0f) },
        { "teal", new Color(0.0f, 0.5f, 0.5f) },
        { "indigo", new Color(0.29f, 0.0f, 0.51f) },
        { "salmon", new Color(0.98f, 0.5f, 0.45f) },
        { "orchid", new Color(0.85f, 0.44f, 0.84f) },
        { "tan", new Color(0.82f, 0.71f, 0.55f) },
        { "silver", new Color(0.75f, 0.75f, 0.75f) },
        { "beige", new Color(0.96f, 0.96f, 0.86f) },
        { "crimson", new Color(0.86f, 0.08f, 0.24f) },
        { "aquamarine", new Color(0.5f, 1.0f, 0.83f) },
        { "coral", new Color(1.0f, 0.5f, 0.31f) },
        { "peach", new Color(1.0f, 0.85f, 0.73f) },
        { "emerald", new Color(0.0f, 0.50f, 0.50f) },
        { "amber", new Color(1.0f, 0.75f, 0.0f) },
        { "wine", new Color(0.50f, 0.0f, 0.0f) },
        { "plum", new Color(0.87f, 0.63f, 0.87f) },
        { "taupe", new Color(0.29f, 0.29f, 0.29f) },
        { "ruby", new Color(0.50f, 0.0f, 0.0f) },
        { "rose", new Color(1.0f, 0.0f, 0.50f) },
        { "seafoam", new Color(0.5f, 1.0f, 0.5f) },
        { "slate", new Color(0.44f, 0.50f, 0.56f) },
        { "pumpkin", new Color(1.0f, 0.5f, 0.0f) },
        { "ultramarine", new Color(0.07f, 0.04f, 0.56f) },
        { "rust", new Color(0.5f, 0.19f, 0.0f) },
        { "iris", new Color(0.6f, 0.0f, 1.0f) },
        { "sienna", new Color(0.63f, 0.32f, 0.18f) },
        { "ecru", new Color(0.94f, 0.90f, 0.55f) },
        { "scarlet", new Color(1.0f, 0.14f, 0.14f) },
        { "cerulean", new Color(0.0f, 0.48f, 0.65f) },
        { "ochre", new Color(0.6f, 0.3f, 0.0f) },
        { "vermilion", new Color(1.0f, 0.4f, 0.0f) }
    };

    private Dictionary<string, PrimitiveType> shapeMap = new Dictionary<string, PrimitiveType>
    {
        { "cube", PrimitiveType.Cube },
        { "cylinder", PrimitiveType.Cylinder },
        { "sphere", PrimitiveType.Sphere }
    };

    public void RunCommand(string command)
    {
        string lowerCommand = command.ToLower();

        // Shape

        foreach (var shapeEntry in shapeMap)
        {
            if (lowerCommand.Contains(shapeEntry.Key))
            {
                BrushSpawner.ChangeToShape(shapeEntry.Value);
                return;
            }
        }

        // Gravity

        if (lowerCommand.Contains("gravity") && lowerCommand.Contains("on"))
        {
            BrushSpawner.EnableGravity();
        } else if (lowerCommand.Contains("gravity") && lowerCommand.Contains("off"))
        {
            BrushSpawner.DisableGravity();
        }

        // Color

        foreach (var colorEntry in colorMap)
        {
            if (lowerCommand.Contains(colorEntry.Key))
            {
                BrushSpawner.ChangeToColor(colorEntry.Value);
                return;
            }
        }

        // Scene
        if (lowerCommand.Contains("balance") && lowerCommand.Contains("beam"))
        {
            SceneManager.LoadScene("BalanceBeam");
        }
        else if (lowerCommand.Contains("bacteria") && lowerCommand.Contains("growth"))
        {
            SceneManager.LoadScene("BacteriaGrowth");
        }
        else if (lowerCommand.Contains("laser") && lowerCommand.Contains("beam"))
        {
            SceneManager.LoadScene("LaserBeam");
        }
        else if (lowerCommand.Contains("solar") && lowerCommand.Contains("system"))
        {
            SceneManager.LoadScene("SolarSystem");
        }
        else if (lowerCommand.Contains("color") && lowerCommand.Contains("mix"))
        {
            SceneManager.LoadScene("ColorMix");
        }
    }
}
