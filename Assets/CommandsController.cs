using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsController : MonoBehaviour
{
    public BrushSpawner BrushSpawner;

    public void RunCommand(string command)
    {
        if (command.ToLower().Contains("cube"))
        {
            BrushSpawner.ChangeToShape(PrimitiveType.Cube);
        }
        else if (command.ToLower().Contains("cylinder"))
        {
            BrushSpawner.ChangeToShape(PrimitiveType.Cylinder);
        }
        else if (command.ToLower().Contains("sphere"))
        {
            BrushSpawner.ChangeToShape(PrimitiveType.Sphere);
        }

        if (command.ToLower().Contains("gravity") && command.ToLower().Contains("on"))
        {
            BrushSpawner.EnableGravity();
        } else if (command.ToLower().Contains("gravity") && command.ToLower().Contains("off"))
        {
            BrushSpawner.DisableGravity();
        }

        if (command.ToLower().Contains("red"))
        {
            BrushSpawner.ChangeToColor(Color.red);
        }
        else if (command.ToLower().Contains("blue"))
        {
            Debug.Log("yes");
            BrushSpawner.ChangeToColor(Color.blue);
        }
        else if (command.ToLower().Contains("black"))
        {
            BrushSpawner.ChangeToColor(Color.black);
        }
        else if (command.ToLower().Contains("green"))
        {
            BrushSpawner.ChangeToColor(Color.green);
        }
        else if (command.ToLower().Contains("gray"))
        {
            BrushSpawner.ChangeToColor(Color.grey);
        }
        else if (command.ToLower().Contains("yellow"))
        {
            BrushSpawner.ChangeToColor(Color.yellow);
        }
        else if (command.ToLower().Contains("white"))
        {
            BrushSpawner.ChangeToColor(Color.white);
        }
    }
}
