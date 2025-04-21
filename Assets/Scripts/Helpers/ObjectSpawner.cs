using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner
{
    public T Spawn<T>(T prefab, Vector3 position, Transform parent) where T : MonoBehaviour
    {
        return GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
    }
}
