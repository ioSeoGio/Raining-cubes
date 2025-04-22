using UnityEngine;

public interface IPoolableObject
{
    event System.Action<IPoolableObject> OnReturnToPoolRequested;
    void ResetState();
    GameObject gameObject { get; }
}