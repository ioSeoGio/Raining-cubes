using UnityEngine;

public interface IPoolableObject
{
    event System.Action<IPoolableObject> ReturnToPoolRequested;
    void ResetState();
    GameObject gameObject { get; }
}