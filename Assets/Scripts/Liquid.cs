using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Liquid class type which should be added and set to a container which should have a liquid.
/// </summary>
public class Liquid : MonoBehaviour
{
    [SerializeField] private string _name = "default";
    [SerializeField] private Color _color = Color.white;
    [SerializeField] [TextArea] private string _description = "default description";

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public Color Color
    { 
        get { return _color; } 
        set { _color = value; } 
    }
    public string Description 
    { 
        get { return _description; } 
        set { _description = value; } 
    }

    Liquid(string name, Color color, string description)
    {
        Name = name;
        Color = color;
        Description = description;
    }
}
