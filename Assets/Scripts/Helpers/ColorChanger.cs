using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger
{
    private Renderer _renderer;

    public ColorChanger(Renderer renderer)
    {
        _renderer = renderer;
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }
}
