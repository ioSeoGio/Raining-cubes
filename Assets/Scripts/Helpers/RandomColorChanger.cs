using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomColorChanger
{
    public void ChangeColor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
