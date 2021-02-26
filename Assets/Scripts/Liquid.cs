using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public string Description { get; set; }

    Liquid(string name, Color color, string descrition)
    {
        Name = name;
        Color = color;
        Description = descrition;
    }
}
